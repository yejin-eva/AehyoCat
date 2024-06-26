using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.UI;

public class VivoxTextChatUI : MonoBehaviour
{
    public Action unSelectAllUsers;
    public VivoxUserPrefab ToggledParticipant { get => toggledParticipant; set => toggledParticipant = value; }

    [SerializeField] private GameObject chatContent;
    [SerializeField] private GameObject messageObject;
    [SerializeField] private Button enterButton;
    [SerializeField] private Button sendTTSButton;
    [SerializeField] private TMPro.TMP_InputField messageInputField;
    [SerializeField] private TMPro.TextMeshProUGUI messageReceiverText;

    private VivoxUserPrefab toggledParticipant = null;
    private IList<KeyValuePair<string, VivoxMessageObjectUI>> messageObjectPool = new List<KeyValuePair<string, VivoxMessageObjectUI>>();
    private ScrollRect textChatScrollRect;
    private Task FetchMessages = null;
    private DateTime? oldestMessage = null;

    private void Start()
    {
        VivoxService.Instance.ChannelJoined += OnChannelJoined;
        VivoxService.Instance.DirectedMessageReceived += OnDirectedMessageReceived;
        VivoxService.Instance.ChannelMessageReceived += OnChannelMessageReceived;

        textChatScrollRect = GetComponent<UnityEngine.UI.ScrollRect>();

        enterButton.onClick.AddListener(SendMessage);
        sendTTSButton.onClick.AddListener(SubmitTTSMessageToVivox);

        messageInputField.onEndEdit.AddListener((string text) => { EnterKeyOnTextField(); });
        
        textChatScrollRect.onValueChanged.AddListener(ScrollRectChange);
    }

    private void SubmitTTSMessageToVivox()
    {
        if (string.IsNullOrEmpty(messageInputField.text))
        {
            return;
        }
        VivoxService.Instance.TextToSpeechSendMessage(messageInputField.text, TextToSpeechMessageType.RemoteTransmissionWithLocalPlayback);
        ClearTextField();
    }

    private void OnEnable()
    {
        ClearTextField();
    }
    

    private void ScrollRectChange(Vector2 arg0)
    {
        //scrolled near end and check if we fetched history already
        if (textChatScrollRect.verticalNormalizedPosition >= 0.95f && FetchMessages != null && (FetchMessages.IsCompleted || FetchMessages.IsFaulted || FetchMessages.IsCanceled))
        {
            textChatScrollRect.normalizedPosition = new Vector2(0, 0.8f);
            FetchMessages = FetchHistory(false);
        }
    }

    private async Task FetchHistory(bool scrollToBottom = false)
    {
        try
        {
            var chatHistoryOptions = new ChatHistoryQueryOptions() 
            { 
                TimeEnd = oldestMessage
            };

            var historyMessages = await VivoxService.Instance.GetChannelTextMessageHistoryAsync(VivoxVoiceManager.LobbyChannelName, 10, chatHistoryOptions);
            var reversedMessages = historyMessages.Reverse();
            foreach(var historyMessage in reversedMessages)
            {
                AddMessageToChat(historyMessage, true, scrollToBottom);
            }
            //update oldest message receivedtime if it exists 
            oldestMessage = historyMessages.FirstOrDefault()?.ReceivedTime;

        }
        catch (TaskCanceledException e)
        {
            Debug.Log($"Chat history request was canceled, likely because of a logout or the data is no longer needed: {e.Message}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Tried to fetch chat history and failed with error: {e.Message}");
        }
    }

    private void OnDestroy()
    {
        if (messageObjectPool.Count > 0)
        {
            ClearMessageObjectPool();
        }
        oldestMessage = null;

        VivoxService.Instance.ChannelJoined -= OnChannelJoined;
        VivoxService.Instance.DirectedMessageReceived -= OnDirectedMessageReceived;
        VivoxService.Instance.ChannelMessageReceived -= OnChannelMessageReceived;
        

        textChatScrollRect.onValueChanged.RemoveAllListeners();
    }

    private void ClearMessageObjectPool()
    {
        foreach(var keyValuePair in messageObjectPool)
        {
            Destroy(keyValuePair.Value.gameObject);
        }
        messageObjectPool.Clear();
    }

    private void ClearTextField()
    {
        messageInputField.text = string.Empty;
        messageInputField.Select();
        messageInputField.ActivateInputField();
    }

    private void EnterKeyOnTextField()
    {
        if (!Input.GetKeyDown(KeyCode.Return))
        {
            return;
        }
        SendMessage();
    }

    private void SendMessage()
    {
        if (string.IsNullOrEmpty(messageInputField.text))
        {
            return;
        }

        if (toggledParticipant != null)
        {
            VivoxService.Instance.SendDirectTextMessageAsync(toggledParticipant.Participant.PlayerId, messageInputField.text);
            ShowDirectedMessageToSelf(messageInputField.text, toggledParticipant.Participant.DisplayName);
            ClearTextField();
        }
        else
        {
            VivoxService.Instance.SendChannelTextMessageAsync(VivoxManager.LobbyChannelName, messageInputField.text);
            ClearTextField();
        }
        
    }

    private void ShowDirectedMessageToSelf(string text, string displayName)
    {
        var newMessageObj = Instantiate(messageObject, chatContent.transform);
        var newMessageTextObject = newMessageObj.GetComponent<VivoxMessageObjectUI>();

        newMessageTextObject.SetTextMessageToSelf(text, displayName);

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)chatContent.transform);

        StartCoroutine(SendScrollRectToBottom());
        
    }

    IEnumerator SendScrollRectToBottom()
    {
        yield return new WaitForEndOfFrame();

        textChatScrollRect.verticalNormalizedPosition = 0;
        
        yield return null;
    }

    private void OnDirectedMessageReceived(VivoxMessage message)
    {
        AddMessageToChat(message, false, true);
    }
    private void OnChannelJoined(string obj)
    {
        FetchMessages = FetchHistory(true);
    }
    private void OnChannelMessageReceived(VivoxMessage message)
    {
        AddMessageToChat(message, false, true);
    }

    private void AddMessageToChat(VivoxMessage message, bool isHistory = false, bool scrollToBottom = false)
    {
        var newMessageObj = Instantiate(messageObject, chatContent.transform);
        var newMessageTextObject = newMessageObj.GetComponent<VivoxMessageObjectUI>();
        if (isHistory)
        {
            messageObjectPool.Insert(0, new KeyValuePair<string, VivoxMessageObjectUI>(message.MessageId, newMessageTextObject));
            newMessageObj.transform.SetSiblingIndex(0);
        }
        else
        {
            messageObjectPool.Add(new KeyValuePair<string, VivoxMessageObjectUI>(message.MessageId, newMessageTextObject));
        }

        newMessageTextObject.SetTextMessage(message);

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)chatContent.transform);

        if (scrollToBottom)
        {
            StartCoroutine(SendScrollRectToBottom());
        }
    }

    public void OnToggledParticipant(VivoxUserPrefab newToggledParticipant, bool isToggled)
    {
        if (isToggled)
        {
            if (toggledParticipant != null)
            {
                toggledParticipant.ToggleButton.isOn = false;
            }
            messageReceiverText.text = $"To: {newToggledParticipant.Participant.DisplayName}";
            toggledParticipant = newToggledParticipant;
        }
        else
        {
            messageReceiverText.text = "To: All";
            toggledParticipant = null;
        }
    }
}

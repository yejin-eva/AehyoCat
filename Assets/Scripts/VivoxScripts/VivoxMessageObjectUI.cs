using System;
using Unity.Services.Vivox;
using UnityEngine;

public class VivoxMessageObjectUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI messageText;

    private VivoxMessage vivoxMessage;
    public void SetTextMessage(VivoxMessage message)
    {
        this.vivoxMessage = message;
        
        if(message.FromSelf)
        {
            messageText.alignment = TMPro.TextAlignmentOptions.Right;
            messageText.text = string.Format($"{message.MessageText} : <color=blue>{message.SenderDisplayName} </color> \n <color=#5A5A5A><size=8>{message.ReceivedTime}</size></color>");
        }
        else
        {
            messageText.alignment = TMPro.TextAlignmentOptions.Left;
            messageText.text = string.IsNullOrEmpty(message.ChannelName)
                ? string.Format($"<color=purple>{message.SenderDisplayName}'s owner </color>: {message.MessageText}\n<color=#5A5A5A><size=8>{message.ReceivedTime}</size></color>") // DM
                : string.Format($"<color=green>{message.SenderDisplayName}'s owner </color>: {message.MessageText}\n<color=#5A5A5A><size=8>{message.ReceivedTime}</size></color>"); // Channel Message
        }
    }
}
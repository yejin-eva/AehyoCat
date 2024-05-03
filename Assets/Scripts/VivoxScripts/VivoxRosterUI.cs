using System.Collections.Generic;
using Unity.Services.Vivox;
using UnityEngine;

public class VivoxRosterUI : MonoBehaviour
{
    [SerializeField] private GameObject vivoxUserPrefab;
    [SerializeField] private VivoxTextChatUI vivoxTextChatUI;

    private Dictionary<string, VivoxUserPrefab> connectedUsers = new Dictionary<string, VivoxUserPrefab>();

    private void Start()
    {
        VivoxService.Instance.ParticipantAddedToChannel += OnParticipantAddedToChannel;
        VivoxService.Instance.ParticipantRemovedFromChannel += OnParticipantRemovedFromChannel;
    }
    private void OnDestroy()
    {
        VivoxService.Instance.ParticipantAddedToChannel -= OnParticipantAddedToChannel;
        VivoxService.Instance.ParticipantRemovedFromChannel -= OnParticipantRemovedFromChannel;
    }
    private void OnParticipantRemovedFromChannel(VivoxParticipant participant)
    {
        DestroyConnectedUser(participant);
    }

    private void OnParticipantAddedToChannel(VivoxParticipant participant)
    {
        CreateConnectedUser(participant);
    }

    private void CreateConnectedUser(VivoxParticipant participant)
    {
        var vivoxUserObject = Instantiate(vivoxUserPrefab, this.transform);
        var vivoxUser = vivoxUserObject.GetComponent<VivoxUserPrefab>();
        vivoxUser.onToggledParticipant += (toggledParticipant, isToggled) => vivoxTextChatUI.OnToggledParticipant(toggledParticipant, isToggled);

        vivoxUser.Init(participant);

        connectedUsers.Add(participant.PlayerId, vivoxUser);
    }
    private void DestroyConnectedUser(VivoxParticipant participant)
    {
        if (connectedUsers.TryGetValue(participant.PlayerId, out VivoxUserPrefab vivoxUser))
        {
            vivoxUser.onToggledParticipant -= (participant, isToggled) => vivoxTextChatUI.OnToggledParticipant(participant, isToggled);
            connectedUsers.Remove(participant.PlayerId);
            Destroy(vivoxUser.gameObject);
        }
        else
        {
            Debug.Log("User not found: " + participant.DisplayName);
        }
    }
}

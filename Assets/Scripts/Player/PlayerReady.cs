using Mirror;
using UnityEngine;

public class PlayerReady : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnReadyChanged))]
    public bool IsReady;

    [SyncVar]
    public string playerName;

    public override void OnStartClient()
    {
        LobbyUI.Instance?.Refresh();
    }

    public void ToggleReady()
    {
        if (!isLocalPlayer) return;

        CmdSetReady(!IsReady);
    }

    void OnReadyChanged(bool oldValue, bool newValue)
    {
        LobbyUI.Instance?.Refresh();
    }

    [Command]
    public void CmdSetReady(bool ready)
    {
        IsReady = ready;
        GameFlowManager.Instance.CheckAllPlayersReady();
    }


    [Server]
    public void ResetReady()
    {
        IsReady = false;
    }
}

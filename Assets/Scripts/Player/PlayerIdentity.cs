using Mirror;
using UnityEngine;

public class PlayerIdentity : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    public override void OnStartServer()
    {
        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Player " + netId;
        }
    }

    public override void OnStartClient()
    {
        OnNameChanged("", playerName);
    }

    public override void OnStartLocalPlayer()
    {
        string savedName = PlayerPrefs.GetString("playerName", "");

        if (string.IsNullOrWhiteSpace(savedName))
        {
            savedName = "Player " + Random.Range(1, 999);
        }

        CmdSetName(savedName);
    }

    [Command]
    void CmdSetName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            newName = "Player " + netId;
        }

        // opcional: limitar longitud
        if (newName.Length > 16)
            newName = newName.Substring(0, 16);

        playerName = newName;
    }

    void OnNameChanged(string oldName, string newName)
    {
        PlayerNameUI ui = GetComponentInChildren<PlayerNameUI>();
        if (ui != null)
        {
            ui.SetName(newName);
        }
    }
}
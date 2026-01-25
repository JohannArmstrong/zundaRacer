using Mirror;
using UnityEngine;

public class PlayerTeam : NetworkBehaviour
{
    [SyncVar]
    public int TeamId;
}

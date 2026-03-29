using Mirror;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}

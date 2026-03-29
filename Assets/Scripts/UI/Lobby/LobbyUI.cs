using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mirror;
using JetBrains.Annotations;

public class LobbyUI : MonoBehaviour
{
    public static LobbyUI Instance;

    public Transform playersListParent;
    public GameObject playerRowPrefab;
    public Canvas lobbyCanvas;

    void Awake()
    {
        Instance = this;
    }

    public void Refresh()
    {
        MatchState state = GameFlowManager.Instance.State;
        if (state != MatchState.Waiting)
        {
            if (lobbyCanvas.gameObject.activeSelf)
            {
                lobbyCanvas.gameObject.SetActive(false);
            }
            return;
        }

        Debug.Log("Refreshing Lobby UI");

        // Limpiar lista
        foreach (Transform child in playersListParent)
            Destroy(child.gameObject);

        // Buscar jugadores en la escena
        var players = FindObjectsByType<PlayerReady>(FindObjectsSortMode.None);

        foreach (var p in players)
        {
            string name = PlayerPrefs.GetString("playerName", NetworkClient.localPlayer.GetInstanceID().ToString());
            var row = Instantiate(playerRowPrefab, playersListParent);
            row.GetComponent<PlayerRowUI>()
            .Setup(name, p.IsReady);
        }
    }
}

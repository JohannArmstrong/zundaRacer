using Mirror;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReadyButton : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonText;

    public void OnClickReady()
    {
        var localPlayer = NetworkClient.localPlayer;
        if (localPlayer == null) return;

        var ready = localPlayer.GetComponent<PlayerReady>();
        ready.ToggleReady();

        UpdateText(ready);
    }

    private void UpdateText(bool isReady)
    {
        if (buttonText != null)
        {
            buttonText.text = isReady ? "UNREADY" : "READY";
        }
    }

}

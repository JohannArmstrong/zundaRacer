using Mirror;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject optionsCanvas;

    public void OnClickPlay()
    {
        NetworkManager.singleton.StartHost();
    }

    public void OnClickOptions()
    {
        optionsCanvas.SetActive(true);
    }

    public void OnClickCloseOptions()
    {
        optionsCanvas.SetActive(false);
    }
}
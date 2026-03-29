using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapLoader : MonoBehaviour
{
    private const string MAIN_MENU_SCENE = "Main Menu";

    void Start()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE);
    }
}
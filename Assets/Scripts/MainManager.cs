using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class MainManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameClearUI;

    private PlayerHealth localPlayerHealth;
    private bool uiShown;

    void Start()
    {
        gameOverUI.SetActive(false);
        gameClearUI.SetActive(false);
    }

    public void RegisterLocalPlayer(PlayerHealth health)
    {
        localPlayerHealth = health;
    }

    void Update()
    {
        if (uiShown || localPlayerHealth == null) return;

        if (localPlayerHealth.HP <= 0)
        {
            ShowGameOverUI();
        }
    }

    void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
        uiShown = true;
        Time.timeScale = 0f;
    }

    public void ShowGameClearUI()
    {
        gameClearUI.SetActive(true);
        uiShown = true;
        Time.timeScale = 0f;
    }

    public void OnRestart()
    {
        if (!uiShown) return;

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{

    [SerializeField, Header("ゲームオーバーUI")]
    private GameObject gameOverUI;

    [SerializeField, Header("ゲームクリアーUI")]
    private GameObject gameClearUI;

    private GameObject player;
    private bool bShowUI;


    void Start()
    {
        player = FindFirstObjectByType<Player>().gameObject;
        bShowUI = false;
    }

    void Update()
    {
        ShowGameOverUI();
    }


    private void ShowGameOverUI()
    {
        if (player) return;

        gameOverUI.SetActive(true);
        bShowUI = true;
    }


    public void ShowGameClearUI()
    {
        gameClearUI.SetActive(true);
        bShowUI = true;
    }

    public void OnRestart()
    {
        if (!bShowUI) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
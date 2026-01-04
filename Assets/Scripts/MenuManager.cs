using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject playerMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject optionsOverlay;

    enum MenuState
    {
        None,
        PlayerMenu,
        Options
    }

    MenuState currentState = MenuState.None;

    void Start()
    {
        //OpenPlayerMenu();
    }

    // --------- API pública ---------

    public void OpenPlayerMenu()
    {
        SetState(MenuState.PlayerMenu);
    }

    public void OpenOptions()
    {
        SetState(MenuState.Options);
    }

    public void CloseAll()
    {
        SetState(MenuState.None);
    }

    // --------- Lógica central ---------

    void SetState(MenuState state)
    {
        currentState = state;

        playerMenu.SetActive(state == MenuState.PlayerMenu);
        optionsMenu.SetActive(state == MenuState.Options);
        optionsOverlay.SetActive(state == MenuState.Options);

        //Time.timeScale = state == MenuState.None ? 1f : 0f;
    }
}
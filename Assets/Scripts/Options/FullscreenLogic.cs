using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenLogic : MonoBehaviour
{
    public Toggle toggle;

    void Start()
    {
        toggle.isOn = Screen.fullScreen;
    }

    void Update()
    {

    }

    public void ActivateFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }
}
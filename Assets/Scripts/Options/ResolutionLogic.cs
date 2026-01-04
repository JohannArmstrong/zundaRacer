using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionLogic : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Start()
    {
        LookForResolutions();
    }


    public void LookForResolutions()
    {
        // fill the list with supported resolutions
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.value = PlayerPrefs.GetInt("resolutionIndex", resolutions.Length - 1);
        //Debug.Log(resolutions[resolutions.Length - 1].width + "x" + resolutions[resolutions.Length - 1].height);
    }

    public void ChangeResolution(int resolutionIndex)
    {
        PlayerPrefs.SetInt("resolutionIndex", resolutionDropdown.value);

        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
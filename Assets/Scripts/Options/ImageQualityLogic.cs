using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class ImageQualityLogic : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    void Start()
    {
        LookForQualityLevels();
    }


    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index, true);
        PlayerPrefs.SetInt("qualityIndex", index);
    }

    void LookForQualityLevels()
    {
        string[] level = QualitySettings.names;
        dropdown.ClearOptions();
        List<string> options = new List<string>();
        //int currentResolution = 0;

        for (int i = 0; i < level.Length; i++)
        {
            options.Add(level[i]);
        }

        dropdown.AddOptions(options);

        int savedQuality = PlayerPrefs.GetInt("qualityIndex", level.Length - 1);

        dropdown.value = savedQuality;
        dropdown.RefreshShownValue();

        SetQuality(savedQuality);
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SoundVolumeLogic : MonoBehaviour
{
    public Slider slider;
    public TMP_InputField input;

    void Start()
    {
        float prefsVolume = PlayerPrefs.GetFloat("audioVolume", 50.0f);
        slider.SetValueWithoutNotify(prefsVolume);
        input.SetTextWithoutNotify(prefsVolume.ToString());
        AudioListener.volume = prefsVolume / 100f;
    }


    public void ChangeSlider(float vol)
    {
        PlayerPrefs.SetFloat("audioVolume", vol);
        AudioListener.volume = vol / 100f;
        input.SetTextWithoutNotify(vol.ToString());
    }

    public void ChangeInput(string value)
    {
        //Debug.Log("Input: '" + value + "'");
        if (string.IsNullOrWhiteSpace(value))
            return;

        float vol;
        if (!float.TryParse(value, out vol))
            return;

        vol = Mathf.Clamp(vol, 0.0f, 100.0f);

        slider.SetValueWithoutNotify(vol);
        ChangeSlider(vol);
    }
}
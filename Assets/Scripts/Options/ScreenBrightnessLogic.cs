using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class ScreenBrightnessLogic : MonoBehaviour
{
    public Slider slider;
    public TMP_InputField input;
    public Image brightnessPanel;

    void Start()
    {
        float prefsBright = PlayerPrefs.GetFloat("brightness", 100f);
        slider.SetValueWithoutNotify(prefsBright);
        input.SetTextWithoutNotify(prefsBright.ToString());
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, math.abs(slider.value - 100) / 100);
    }


    public void ChangeSlider(float bright)
    {
        PlayerPrefs.SetFloat("brightness", bright);
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, math.abs(bright - 100) / 100);
        input.SetTextWithoutNotify(bright.ToString());
    }

    public void ChangeInput(string value)
    {
        //Debug.Log("Input: '" + value + "'");
        if (string.IsNullOrWhiteSpace(value))
            return;

        float bright;
        if (!float.TryParse(value, out bright))
            return;

        bright = Mathf.Clamp(bright, 0.0f, 100.0f);

        slider.SetValueWithoutNotify(bright);
        ChangeSlider(bright);
    }
}
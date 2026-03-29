using TMPro;
using UnityEngine;

public class PlayerRowUI : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text readyText;

    public void Setup(string name, bool ready)
    {
        nameText.text = name;
        readyText.text = ready ? "READY" : "NOT READY";
    }
}
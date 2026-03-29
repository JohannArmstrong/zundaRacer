using TMPro;
using UnityEngine;

public class PlayerNameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;

    public void SetName(string newName)
    {
        nameText.text = newName;
    }
}
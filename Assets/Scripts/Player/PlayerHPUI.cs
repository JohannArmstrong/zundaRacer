using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerHPUI : MonoBehaviour
{
    [SerializeField] private GameObject hpIcon;

    private PlayerHealth playerHealth;
    private int lastHp = -1;

    public void Init(PlayerHealth health)
    {
        playerHealth = health;
        CreateIcons();
        UpdateIcons();
    }

    void Update()
    {
        if (!playerHealth) return;

        if (playerHealth.HP != lastHp)
        {
            UpdateIcons();
        }
    }

    void CreateIcons()
    {
        for (int i = 0; i < playerHealth.MaxHP; i++)
        {
            Instantiate(hpIcon, transform);
        }
    }

    void UpdateIcons()
    {
        Image[] icons = GetComponentsInChildren<Image>();

        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].gameObject.SetActive(i < playerHealth.HP);
        }

        lastHp = playerHealth.HP;
    }
}

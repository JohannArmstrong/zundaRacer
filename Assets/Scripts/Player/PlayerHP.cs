using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    
    [SerializeField, Header("Icon")]
    private GameObject HPIcon;

    private Player player;
    private int beforeHP;

    void Start()
    {
        player = FindFirstObjectByType<Player>();
        beforeHP = player.GetHP();
        CreateHPIcon();
    }

    void Update()
    {
        ShowHPIcon();
    }

//------start---------------------------------------------------------------------------------

    private void CreateHPIcon()
    {
        for (int i = 0; i < player.GetHP(); i++)
        {
            GameObject playerHPObj = Instantiate(HPIcon);
            playerHPObj.transform.parent = transform;
        }
    }




    //------update---------------------------------

    private void ShowHPIcon()
    {
        if (beforeHP == player.GetHP()) return;

        Image[] icons = transform.GetComponentsInChildren<Image>();

        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].gameObject.SetActive(i < player.GetHP());
        }

        beforeHP = player.GetHP();
    }

}
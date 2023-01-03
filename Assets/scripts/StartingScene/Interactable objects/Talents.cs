using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talents : MonoBehaviour
{
    public Playerinfo playerInfo;

    public Canvas interactNotification;
    public GameObject shopUI; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            interactNotification.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            interactNotification.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (interactNotification.gameObject.activeInHierarchy == true && Input.GetKeyDown(KeyCode.F))
        {
            shopUI.SetActive(true);
            playerInfo.CanAct = false;
        }
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
        playerInfo.CanAct = true;
    }
}

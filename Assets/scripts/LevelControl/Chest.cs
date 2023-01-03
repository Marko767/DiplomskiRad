using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Playerinfo playerInfo;

    public Canvas interactNotification;

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
            playerInfo.playerstats.talentCurrency += 5;
            playerInfo.playerstats.shopCurrency += 7;
            playerInfo.playerstats.exp += 30;

            Destroy(gameObject);
        }
    }
}

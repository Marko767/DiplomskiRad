using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopTemplateButton : MonoBehaviour
{
    public Playerinfo playerinfo;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI description;


   public void Click()
    {
        playerinfo.DiscountShop(int.Parse(cost.text));
        UpdateStats();
    }

    private void UpdateStats()
    {
        string[] array = description.text.Split(' ');
        
        if (array[0] == "HP")
        {
            playerinfo.playerstats.HP += playerinfo.playerstats.Level;
            playerinfo.UpdateJSON();
        }

        if (array[0] == "ATK")
        {
            playerinfo.playerstats.ATK += playerinfo.playerstats.Level;
            playerinfo.UpdateJSON();
        }

        if (array[0] == "DEF")
        {
            playerinfo.playerstats.DEF += playerinfo.playerstats.Level;
            playerinfo.UpdateJSON();
        }

        if (array[0] == "SPD")
        {
            playerinfo.playerstats.SPD += playerinfo.playerstats.Level;
            playerinfo.UpdateJSON();
        }

        if (array[0] == "POTION")
        {
            playerinfo.playerstats.noPotions += 1;
            playerinfo.UpdateJSON();
        }
    }
}

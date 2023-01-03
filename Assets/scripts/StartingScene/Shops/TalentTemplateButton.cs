using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalentTemplateButton : MonoBehaviour
{
    public Playerinfo playerinfo;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI description;


    public void Click()
    {
        playerinfo.DiscountTalent(int.Parse(cost.text));
        UpdateTalents();
    }

    private void UpdateTalents()
    {
        string[] array = description.text.Split(' ');

        if (array[1] == "HP" || array[1] == "heal" || array[1] == "dropping")
        {
            playerinfo.playerstats.HPtalents += 1;
            playerinfo.UpdateJSON();
        }

        if (array[1] == "ATK" || array[1] == "the" || array[1] == "attacking")
        {
            playerinfo.playerstats.ATKtalents += 1;
            playerinfo.UpdateJSON();
        }

        if (array[1] == "DEF" || array[1] == "10%")
        {
            playerinfo.playerstats.DEFtalents += 1;
            playerinfo.UpdateJSON();
        }

        if (array[1] == "SPD" || array[1] == "double" || array[1] == "dash")
        {
            playerinfo.playerstats.SPDtalents += 1;
            playerinfo.UpdateJSON();
        }
    }
}

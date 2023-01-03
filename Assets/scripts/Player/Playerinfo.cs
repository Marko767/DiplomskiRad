using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Playerinfo : MonoBehaviour
{
    public TextAsset JSONinfo;
    string path = @"C:\UnityProjects\MyGame\Assets\scripts\Player\JSONstats.txt";

    public class playerStats
    {
        public int HP;
        public int ATK;
        public int DEF;
        public int SPD;

        public int Level;

        public int talentCurrency;
        public int shopCurrency;

        public int noPotions;

        public int ATKtalents;
        public int DEFtalents;
        public int HPtalents;
        public int SPDtalents;

        public int exp;
    }


    public enum State {still, moveing, jumping, attacking};
    public bool CanAct = true;

    public PlayerInfoPanel infoPanel;

    public playerStats playerstats = new playerStats();
    
    public void DiscountTalent(int price)
    {
        playerstats.talentCurrency -= price;
    }

    public void DiscountShop(int price)
    {
        playerstats.shopCurrency -= price;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (infoPanel.gameObject.activeSelf == false)
            {
                infoPanel.gameObject.SetActive(true);

            }
            else if (infoPanel.gameObject.activeSelf == true)
            {
                infoPanel.gameObject.SetActive(false);

            }
        }

        if(playerstats.exp > 100)
        {
            playerstats.exp -= 100;
            playerstats.Level += 1;
            UpdateJSON();
        }
    }

    private void Start()
    {
        //playerstats.HP = 100;
        //playerstats.ATK = 10;
        //playerstats.DEF = 10;
        //playerstats.SPD = 10;

        //playerstats.Level = 1;

        //playerstats.talentCurrency = 100;
        //playerstats.shopCurrency = 100;

        //playerstats.noPotions = 1;

        //playerstats.ATKtalents = 0;
        //playerstats.DEFtalents = 0;
        //playerstats.HPtalents = 0;
        //playerstats.SPDtalents = 0;

        playerstats = JsonUtility.FromJson<playerStats>(JSONinfo.text);
    }

    public void UpdateJSON()
    {
        string json = JsonUtility.ToJson(playerstats);

        File.WriteAllText(path, json);
    }
}

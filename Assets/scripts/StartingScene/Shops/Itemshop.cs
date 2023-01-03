using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Itemshop : MonoBehaviour
{
    public Playerinfo playerInfo;

    public GameObject container;
    public GameObject talentTemplate;
    public TextMeshProUGUI currency;

    float talentSpace = 60f;

    private void Awake()
    {
        talentTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        currency.text = playerInfo.playerstats.shopCurrency.ToString();

        CreateTalentInShop("HP +" + (10 * playerInfo.playerstats.Level).ToString(), playerInfo.playerstats.Level * 10, 0);
        CreateTalentInShop("ATK +" + playerInfo.playerstats.Level.ToString(), playerInfo.playerstats.Level * 10, 1);
        CreateTalentInShop("DEF +" + playerInfo.playerstats.Level.ToString(), playerInfo.playerstats.Level * 10, 2);
        CreateTalentInShop("SPD +" + playerInfo.playerstats.Level.ToString(), playerInfo.playerstats.Level * 10, 3);
        CreateTalentInShop("POTION", 20, 4);

    }

    private void Update()
    {
        currency.text = playerInfo.playerstats.shopCurrency.ToString();
    }

    private void CreateTalentInShop(string name, int cost, int index)
    {
        talentTemplate.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = name;
        talentTemplate.transform.Find("Cost").GetComponent<TextMeshProUGUI>().text = cost.ToString();

        GameObject talentShop = Instantiate(talentTemplate, container.GetComponent<Transform>());
        Transform talentShopTrans = talentShop.GetComponent<Transform>();

        talentShopTrans.localPosition = new Vector2(0, talentTemplate.transform.localPosition.y - talentSpace * index);
        talentShop.SetActive(true);
    }
}

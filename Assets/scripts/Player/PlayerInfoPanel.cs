using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInfoPanel : MonoBehaviour
{
    public Playerinfo playerInfo;

    public GameObject container;
    public GameObject statTemplate;

    public Sprite[] iconSprites;

    float talentSpace = 60f;

    public TextMeshProUGUI goldCurrency;
    public TextMeshProUGUI talentCurrency;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI level;

    private List<GameObject> shopTemplates = new List<GameObject>();

    private void Awake()
    {
        statTemplate.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if(shopTemplates.Count > 0)
        {
            foreach (GameObject temp in shopTemplates)
            {
                Destroy(temp);
            }
        }
        

        CreateTalentInShop(playerInfo.playerstats.HP, 0);
        CreateTalentInShop(playerInfo.playerstats.ATK, 1);
        CreateTalentInShop(playerInfo.playerstats.DEF, 2);
        CreateTalentInShop(playerInfo.playerstats.SPD, 3);
        CreateTalentInShop(playerInfo.playerstats.noPotions, 4);

        Name.text = "Marko";
        level.text = "Level:" + playerInfo.playerstats.Level.ToString();
    }

    private void Update()
    {
        goldCurrency.text = playerInfo.playerstats.shopCurrency.ToString();
        talentCurrency.text = playerInfo.playerstats.talentCurrency.ToString();
    }

    private void CreateTalentInShop(int Value, int index)
    {
        statTemplate.transform.Find("StatValue").GetComponent<TextMeshProUGUI>().text = Value.ToString();
        statTemplate.transform.Find("StatIcon").GetComponent<Image>().sprite = iconSprites[index];

        if(index == 0 || index == 2 || index == 4)
        {
            statTemplate.transform.Find("StatIcon").GetComponent<RectTransform>().localScale = new Vector3(2, 2, 1);
        }
        else
        {
            statTemplate.transform.Find("StatIcon").GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }


        GameObject StatDisplay = Instantiate(statTemplate, container.GetComponent<Transform>());
        Transform StatDisplayTrans = StatDisplay.GetComponent<Transform>();

        StatDisplayTrans.localPosition = new Vector2(0, statTemplate.transform.localPosition.y - talentSpace * index);
        StatDisplay.SetActive(true);

        shopTemplates.Add(StatDisplay);
    }

    public void CloseStats()
    {
        gameObject.SetActive(false);
    }
}

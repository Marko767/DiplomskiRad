using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Talentshop : MonoBehaviour
{
    public Playerinfo playerInfo;

    public GameObject container;
    public GameObject talentTemplate;
    public TextMeshProUGUI currency;

    private List<Talent> HPtalentList = new List<Talent>();
    private List<Talent> ATKtalentList = new List<Talent>();
    private List<Talent> DEFtalentList = new List<Talent>();
    private List<Talent> SPDtalentList = new List<Talent>();

    float talentSpace = 60f;

    private void Awake()
    {
        talentTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        currency.text = playerInfo.playerstats.talentCurrency.ToString();
  
    }

    private void OnEnable()
    {
        HPtalentListFill();
        ATKtalentListFill();
        DEFtalentListFill();
        SPDtalentListFill();

        if (playerInfo.playerstats.HPtalents != 3)
        {
            CreateTalentInShop(HPtalentList[playerInfo.playerstats.HPtalents].Description, HPtalentList[playerInfo.playerstats.HPtalents].cost, 0);
        }
        if (playerInfo.playerstats.ATKtalents != 3)
        {
            CreateTalentInShop(ATKtalentList[playerInfo.playerstats.ATKtalents].Description, ATKtalentList[playerInfo.playerstats.ATKtalents].cost, 1);
        }
        if (playerInfo.playerstats.DEFtalents != 3)
        {
            CreateTalentInShop(DEFtalentList[playerInfo.playerstats.DEFtalents].Description, DEFtalentList[playerInfo.playerstats.DEFtalents].cost, 2);
        }
        if (playerInfo.playerstats.SPDtalents != 3)
        {
            CreateTalentInShop(SPDtalentList[playerInfo.playerstats.SPDtalents].Description, SPDtalentList[playerInfo.playerstats.SPDtalents].cost, 3);
        }
    }

    private void Update()
    {
        currency.text = playerInfo.playerstats.talentCurrency.ToString();
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

    #region Talent list fill

    private void HPtalentListFill()
    {
        Talent HPlvl1 = new Talent();
        HPlvl1.Description = "Potions heal for 25% more";
        HPlvl1.cost = 10;
        HPlvl1.owned = false;
        HPtalentList.Add(HPlvl1);

        Talent HPlvl2 = new Talent();
        HPlvl2.Description = "+5% HP";
        HPlvl2.cost = 25;
        HPlvl2.owned = false;
        HPtalentList.Add(HPlvl2);

        Talent HPlvl3 = new Talent();
        HPlvl3.Description = "When dropping at 30% gain imunity 2 sec";
        HPlvl3.cost = 50;
        HPlvl3.owned = false;
        HPtalentList.Add(HPlvl3);
    }

    private void ATKtalentListFill()
    {
        Talent ATKlvl1 = new Talent();
        ATKlvl1.Description = "Reduce the CD of ATK by 20%";
        ATKlvl1.cost = 10;
        ATKlvl1.owned = false;
        ATKtalentList.Add(ATKlvl1);

        Talent ATKlvl2 = new Talent();
        ATKlvl2.Description = "+5% ATK";
        ATKlvl2.cost = 25;
        ATKlvl2.owned = false;
        ATKtalentList.Add(ATKlvl2);

        Talent ATKlvl3 = new Talent();
        ATKlvl3.Description = "Enables attacking from both sides";
        ATKlvl3.cost = 50;
        ATKlvl3.owned = false;
        ATKtalentList.Add(ATKlvl3);
    }

    private void DEFtalentListFill()
    {
        Talent DEFlvl1 = new Talent();
        DEFlvl1.Description = "Gain 10% of DEF as ATK";
        DEFlvl1.cost = 10;
        DEFlvl1.owned = false;
        DEFtalentList.Add(DEFlvl1);

        Talent DEFlvl2 = new Talent();
        DEFlvl2.Description = "+5% DEF";
        DEFlvl2.cost = 25;
        DEFlvl2.owned = false;
        DEFtalentList.Add(DEFlvl2);

        Talent DEFlvl3 = new Talent();
        DEFlvl3.Description = "Return 10% of damage you recieve";
        DEFlvl3.cost = 50;
        DEFlvl3.owned = false;
        DEFtalentList.Add(DEFlvl3);
    }

    private void SPDtalentListFill()
    {
        Talent SPDlvl1 = new Talent();
        SPDlvl1.Description = "Reduces dash CD by 25%";
        SPDlvl1.cost = 10;
        SPDlvl1.owned = false;
        SPDtalentList.Add(SPDlvl1);

        Talent SPDlvl2 = new Talent();
        SPDlvl2.Description = "+5% SPD";
        SPDlvl2.cost = 25;
        SPDlvl2.owned = false;
        SPDtalentList.Add(SPDlvl2);

        Talent SPDlvl3 = new Talent();
        SPDlvl3.Description = "Enables double jump";
        SPDlvl3.cost = 50;
        SPDlvl3.owned = false;
        SPDtalentList.Add(SPDlvl3);
    }

    #endregion
}

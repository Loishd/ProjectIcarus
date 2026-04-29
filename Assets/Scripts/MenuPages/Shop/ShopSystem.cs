using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] TMP_Text CoinText;
    private float overallCoin;

    public List<GameObject> BuffPages = new List<GameObject>();
    public List<GameObject> GadgetPages = new List<GameObject>();

    [SerializeField] TMP_Text invulnerabilityPriceText;
    [SerializeField] TMP_Text attractionPriceText;
    [SerializeField] TMP_Text heatShieldPriceText;
    [SerializeField] TMP_Text flapModPriceText;
    [SerializeField] TMP_Text diveModPriceText;
    [SerializeField] TMP_Text planeModPriceText;

    [SerializeField] TMP_Text invulnerabilityAmountText;
    [SerializeField] TMP_Text attractionAmountText;
    [SerializeField] TMP_Text heatShieldAmountText;

    [SerializeField] TMP_Text flapEquipText;
    [SerializeField] TMP_Text diveEquipText;
    [SerializeField] TMP_Text planeEquipText;

    int invulreabilityAmount;
    int attractionAmount;
    int heatShieldAmount;

    public bool equipGadget1;
    public bool equipGadget2;
    public bool equipGadget3;

    [Header("Buff Price (D)")]
    [SerializeField] int invulnerabilityPrice;
    [SerializeField] int attractionPrice;
    [SerializeField] int heatShieldPrice;
    [SerializeField] int flapModulePrice;
    [SerializeField] int diveModulePrice;
    [SerializeField] int planeModulePrice;

    private int currentPage;
    private bool isBuff;
    private bool isGadget;

    void Start()
    {
        currentPage = 0;
        isBuff = true;

        overallCoin = PlayerPrefs.GetFloat("CoinAmount");
        UpdateCoinText();

        invulnerabilityPriceText.text = invulnerabilityPrice.ToString();
        attractionPriceText.text = attractionPrice.ToString();
        heatShieldPriceText.text = heatShieldPrice.ToString();
        flapModPriceText.text = flapModulePrice.ToString();
        diveModPriceText.text = diveModulePrice.ToString();
        planeModPriceText.text = planeModulePrice.ToString();

        invulreabilityAmount = PlayerPrefs.GetInt("BoughtInvulnerability");
        attractionAmount = PlayerPrefs.GetInt("BoughtAttraction");
        heatShieldAmount = PlayerPrefs.GetInt("BoughtHeatShield");

        invulnerabilityAmountText.text = "x" + invulreabilityAmount.ToString();
        attractionAmountText.text = "x" + attractionAmount.ToString();
        heatShieldAmountText.text = "x" + heatShieldAmount.ToString();

        RevealEquipButton();
    }

    void UpdateCoinText()
    {
        CoinText.text = "Your Coins : " + overallCoin.ToString();
    }

    public void NextPage()
    {
        if (isBuff)
        {
            BuffPages[currentPage].SetActive(false);
            currentPage++;

            if (currentPage >= BuffPages.Count)
            {
                currentPage = 0;
            }

            BuffPages[currentPage].SetActive(true);
        }

        else if (isGadget)
        {
            GadgetPages[currentPage].SetActive(false);
            currentPage++;

            if (currentPage >= GadgetPages.Count)
            {
                currentPage = 0;
            }

            GadgetPages[currentPage].SetActive(true);
        }
    }

    public void PreviousPage()
    {
        if (isBuff)
        {
            BuffPages[currentPage].SetActive(false);
            currentPage--;

            if (currentPage < 0)
            {
                currentPage = BuffPages.Count - 1;
            }

            BuffPages[currentPage].SetActive(true);
        }

        else if (isGadget)
        {
            GadgetPages[currentPage].SetActive(false);
            currentPage--;

            if (currentPage < 0)
            {
                currentPage = GadgetPages.Count - 1;
            }

            GadgetPages[currentPage].SetActive(true);
        }
    }

    public void BuffButton()
    {
        isGadget = false;
        GadgetPages[currentPage].SetActive(false);

        isBuff = true;
        currentPage = 0;
        BuffPages[currentPage].SetActive(true);
    }

    public void GadgetButton()
    {
        isBuff = false;
        BuffPages[currentPage].SetActive(false);

        isGadget = true;
        currentPage = 0;
        GadgetPages[currentPage].SetActive(true);
    }

    public void Buy(int itemIndex)
    {
        int price = 999999;
        itemIndex -= 1;

        if (itemIndex == 0)
            price = invulnerabilityPrice;

        else if (itemIndex == 1)
            price = attractionPrice;

        else if (itemIndex == 2)
            price = heatShieldPrice;

        if (overallCoin >= price)
        {
            Debug.Log("Sold!");
            overallCoin -= price;
            GiveItem(itemIndex);
            UpdateCoinText();
            UpdateCoin();
        }
        else
        {
            Debug.Log("Not Enough Money...");
            throw new System.Exception();
        }
    }

    public void GiveItem(int itemIndex)
    {
        if (itemIndex == 0)
        {
            Debug.Log("Give Invulnerability!");
            PlayerPrefs.SetInt("BoughtInvulnerability", invulreabilityAmount++);
            invulnerabilityAmountText.text = "x" + invulreabilityAmount.ToString();
            
        }
        else if (itemIndex == 1)
        {
            Debug.Log("Give Attraction!");
            PlayerPrefs.SetInt("BoughtAttraction", attractionAmount++);
            attractionAmountText.text = "x" + attractionAmount.ToString();
            
        }
        else if (itemIndex == 2)
        {
            Debug.Log("Give HeatShield!");
            PlayerPrefs.SetInt("BoughtHeatShield", heatShieldAmount++);
            heatShieldAmountText.text = "x" + heatShieldAmount.ToString();

        }
    }

    public void BuyGadget(int gadgetIndex)
    {
        int price = 999999;
        gadgetIndex -= 1;

        if (gadgetIndex == 0)
        {
            price = flapModulePrice;

            if (PlayerPrefs.GetInt("CanEquipFlapModule") == 1) return;
        }
            
        else if (gadgetIndex == 1)
        {
            price = diveModulePrice;

            if (PlayerPrefs.GetInt("CanEquipDiveModule") == 1) return;
        }

        else if (gadgetIndex == 2)
        {
            price = planeModulePrice;

            if (PlayerPrefs.GetInt("CanEquipPlaneModule") == 1) return;
        }

        if (overallCoin >= price)
        {
            Debug.Log("Sold!");
            overallCoin -= price;
            MarkGadgetToEquipable(gadgetIndex);
            UpdateCoinText();
            UpdateCoin();
        }
        else
        {
            Debug.Log("Not Enough Money...");
            throw new System.Exception();
        }
    }

    public void MarkGadgetToEquipable(int gadgetIndex)
    {
        if (gadgetIndex == 0)
        {
            Debug.Log("Give Flap module!");
            PlayerPrefs.SetInt("CanEquipFlapModule", 1);
            
        }
        else if (gadgetIndex == 1)
        {
            Debug.Log("Give Dive module!");
            PlayerPrefs.SetInt("CanEquipDiveModule", 1);
            
        }
        else if (gadgetIndex == 2)
        {
            Debug.Log("Give Plane mode!");
            PlayerPrefs.SetInt("CanEquipPlaneModule", 1);
            
        }
        RevealEquipButton();

    }
    

    public void RevealEquipButton()
    {
        if (PlayerPrefs.GetInt("CanEquipFlapModule") == 1)
        {
            if (PlayerPrefs.GetInt("EquippedFlapModule") == 1)
            {
                flapEquipText.text = "UNEQUIP";
                equipGadget1 = true;
            }
        }

        if (PlayerPrefs.GetInt("CanEquipDiveModule") == 1)
        {

            if (PlayerPrefs.GetInt("EquippedDiveModule") == 1)
            {
                diveEquipText.text = "UNEQUIP";
                equipGadget2 = true;
            }

        }

        if (PlayerPrefs.GetInt("CanEquipPlaneModule") == 1)
        {

            if (PlayerPrefs.GetInt("EquippedPlaneModule") == 1)
            {
                planeEquipText.text = "UNEQUIP";
                equipGadget3 = true;
            }
        }
    }

    public void EquipGadget(int gadgetIndex)
    {
        
        if (gadgetIndex == 0 && PlayerPrefs.GetInt("CanEquipFlapModule") == 1)
        {
            if (PlayerPrefs.GetInt("EquippedFlapModule") == 0)
            {
                UnequipAllGadget();
                PlayerPrefs.SetInt("EquippedFlapModule", 1);

                flapEquipText.text = "UNEQUIP";
                equipGadget1 = true;
            }
            else
            {
                PlayerPrefs.SetInt("EquippedFlapModule", 0);

                flapEquipText.text = "EQUIP";
                equipGadget1 = false;
            }
        }

        if (gadgetIndex == 1 && PlayerPrefs.GetInt("CanEquipDiveModule") == 1)
        {
            if (PlayerPrefs.GetInt("EquippedDiveModule") == 0)
            {
                UnequipAllGadget();
                PlayerPrefs.SetInt("EquippedDiveModule", 1);

                diveEquipText.text = "UNEQUIP";
                equipGadget2 = true;
            }
            else
            {
                PlayerPrefs.SetInt("EquippedDiveModule", 0);

                diveEquipText.text = "EQUIP";
                equipGadget2 = false;
            }
        }

        if (gadgetIndex == 2 && PlayerPrefs.GetInt("CanEquipPlaneModule") == 1)
        {
            if (PlayerPrefs.GetInt("EquippedPlaneModule") == 0)
            {
                UnequipAllGadget();
                PlayerPrefs.SetInt("EquippedPlaneModule", 1);

                planeEquipText.text = "UNEQUIP";
                equipGadget3 = true;
            }
            else
            {
                PlayerPrefs.SetInt("EquippedPlaneModule", 0);

                planeEquipText.text = "EQUIP";
                equipGadget3 = false;
            }
        }
    }

    void UnequipAllGadget()
    {
        equipGadget1 = false;
        equipGadget2 = false;
        equipGadget3 = false;

        flapEquipText.text = "EQUIP";
        diveEquipText.text = "EQUIP";
        planeEquipText.text = "EQUIP";

        PlayerPrefs.SetInt("EquippedFlapModule", 0);
        PlayerPrefs.SetInt("EquippedDiveModule", 0);
        PlayerPrefs.SetInt("EquippedPlaneModule", 0);
    }

    void UpdateCoin()
    {
        PlayerPrefs.SetFloat("CoinAmount", overallCoin);
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] TMP_Text CoinText;
    private float overallCoin;

    [SerializeField] TMP_Text invulnerabilityPriceText;
    [SerializeField] TMP_Text attractionPriceText;
    [SerializeField] TMP_Text heatShieldPriceText;
    [SerializeField] TMP_Text invulnerabilityAmountText;
    [SerializeField] TMP_Text attractionAmountText;
    [SerializeField] TMP_Text heatShieldAmountText;
    int invulreabilityAmount;
    int attractionAmount;
    int heatShieldAmount;

    [SerializeField] Button equipbutton1;
    [SerializeField] Button equipbutton2;
    [SerializeField] Button equipbutton3;

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

    void Start()
    {
        overallCoin = PlayerPrefs.GetFloat("CoinAmount");
        UpdateCoinText();

        invulnerabilityPriceText.text = invulnerabilityPrice.ToString();
        attractionPriceText.text = attractionPrice.ToString();
        heatShieldPriceText.text = heatShieldPrice.ToString();

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
            equipbutton1.gameObject.SetActive(true);

            if (PlayerPrefs.GetInt("EquippedFlapModule") == 1)
            {
                Image image = equipbutton1.GetComponent<Image>();
                image.color = Color.red;
                equipGadget1 = true;
            }
        }

        if (PlayerPrefs.GetInt("CanEquipDiveModule") == 1)
        {
            equipbutton2.gameObject.SetActive(true);

            if (PlayerPrefs.GetInt("EquippedDiveModule") == 1)
            {
                Image image = equipbutton2.GetComponent<Image>();
                image.color = Color.red;
                equipGadget2 = true;
            }

        }

        if (PlayerPrefs.GetInt("CanEquipPlaneModule") == 1)
        {
            equipbutton3.gameObject.SetActive(true);

            if (PlayerPrefs.GetInt("EquippedPlaneModule") == 1)
            {
                Image image = equipbutton3.GetComponent<Image>();
                image.color = Color.red;
                equipGadget3 = true;
            }
        }
    }

    public void EquipGadget(int gadgetIndex)
    {
        
        if (gadgetIndex == 0 && PlayerPrefs.GetInt("CanEquipFlapModule") == 1)
        {
            if (PlayerPrefs.GetInt("EquippedFlapModule") == 0 && !equipGadget2 && !equipGadget3)
            {
                PlayerPrefs.SetInt("EquippedFlapModule", 1);

                Image image = equipbutton1.GetComponent<Image>();
                image.color = Color.red;
                equipGadget1 = true;
            }
            else
            {
                PlayerPrefs.SetInt("EquippedFlapModule", 0);

                Image image = equipbutton1.GetComponent<Image>();
                image.color = Color.green;
                equipGadget1 = false;
            }
        }

        if (gadgetIndex == 1 && PlayerPrefs.GetInt("CanEquipDiveModule") == 1)
        {
            if (PlayerPrefs.GetInt("EquippedDiveModule") == 0 && !equipGadget1 && !equipGadget3)
            {
                PlayerPrefs.SetInt("EquippedDiveModule", 1);

                Image image = equipbutton2.GetComponent<Image>();
                image.color = Color.red;
                equipGadget2 = true;
            }
            else
            {
                PlayerPrefs.SetInt("EquippedDiveModule", 0);

                Image image = equipbutton2.GetComponent<Image>();
                image.color = Color.green;
                equipGadget2 = false;
            }
        }

        if (gadgetIndex == 2 && PlayerPrefs.GetInt("CanEquipPlaneModule") == 1)
        {
            if (PlayerPrefs.GetInt("EquippedPlaneModule") == 0 && !equipGadget1 && !equipGadget2)
            {
                PlayerPrefs.SetInt("EquippedPlaneModule", 1);

                Image image = equipbutton3.GetComponent<Image>();
                image.color = Color.red;
                equipGadget3 = true;
            }
            else
            {
                PlayerPrefs.SetInt("EquippedPlaneModule", 0);

                Image image = equipbutton3.GetComponent<Image>();
                image.color = Color.green;
                equipGadget3 = false;
            }
        }
    }

    void UpdateCoin()
    {
        PlayerPrefs.SetFloat("CoinAmount", overallCoin);
    }

}

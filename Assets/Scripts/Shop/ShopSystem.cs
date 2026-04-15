using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    [Header("Buff Price (D)")]
    [SerializeField] int invulnerabilityPrice;
    [SerializeField] int attractionPrice;
    [SerializeField] int heatShieldPrice;

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

    void UpdateCoin()
    {
        PlayerPrefs.SetFloat("CoinAmount", overallCoin);
    }

}

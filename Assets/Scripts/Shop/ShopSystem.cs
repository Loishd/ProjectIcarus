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
            if (itemIndex == 0 && PlayerPrefs.GetInt("BoughtInvulnerability") == 1) return;
            if (itemIndex == 1 && PlayerPrefs.GetInt("BoughtAttraction") == 1) return;
            if (itemIndex == 2 && PlayerPrefs.GetInt("BoughtHeatShield") == 1) return;

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
            PlayerPrefs.SetInt("BoughtInvulnerability", 1);
        }
        else if (itemIndex == 1)
        {
            Debug.Log("Give Attraction!");
            PlayerPrefs.SetInt("BoughtAttraction", 1);
        }
        else if (itemIndex == 2)
        {
            Debug.Log("Give HeatShield!");
            PlayerPrefs.SetInt("BoughtHeatShield", 1);
        }
    }

    void UpdateCoin()
    {
        PlayerPrefs.SetFloat("CoinAmount", overallCoin);
    }
}

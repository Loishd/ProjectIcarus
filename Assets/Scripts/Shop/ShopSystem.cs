using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] TMP_Text CoinText;
    private float overallCoin;

    void Start()
    {
        overallCoin = PlayerPrefs.GetFloat("CoinAmount");
        UpdateCoinText();
    }

    void Update()
    {
        
    }

    void UpdateCoinText()
    {
        CoinText.text = "Your Coins : " + overallCoin.ToString();
    }

    public void Buy(int price)
    {
        if (overallCoin >= price)
        {
            Debug.Log("Sold!");
            overallCoin -= price;
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
        else if (itemIndex == 3)
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

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
}

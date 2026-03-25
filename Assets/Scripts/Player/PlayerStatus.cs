using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public bool isDeath = false;
    public bool isInvulnerability;
    public bool isMagnetic;
    public bool isFever;

    public static PlayerStatus Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Destroy(gameObject); // sometimes causes issues
        }

        isFever = false;
        isInvulnerability = false;
    }

    public void AddCoinToPlayer(float amount)
    {
        float overallCoin = PlayerPrefs.GetFloat("CoinAmount");

        PlayerPrefs.SetFloat("CoinAmount", amount + overallCoin);
    }
}

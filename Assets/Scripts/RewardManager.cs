using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] float TotalCoins;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TotalCoins += PlayerPrefs.GetFloat("CoinAmount");
        HoarderNextToPlutus();
    }

    void HoarderNextToPlutus()
    {
        if (TotalCoins >= 9999)
        {
            Debug.Log("Hoarder next to Plutus Completed!");
        }
    }
}

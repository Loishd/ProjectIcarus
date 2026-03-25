using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saves : MonoBehaviour
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("HasLaunchedBefore"))
        {
            //Stats
            PlayerPrefs.SetFloat("HighestScore", 0);
            PlayerPrefs.SetFloat("CoinAmount", 0);

            //Shop
            PlayerPrefs.SetInt("BoughtInvulnerability", 0);
            PlayerPrefs.SetInt("BoughtAttraction", 0);
            PlayerPrefs.SetInt("BoughtHeatShield", 0);

            //Misc.
            PlayerPrefs.SetInt("HasLaunchedBefore", 1);
            PlayerPrefs.Save();
        }
    }

    void Update()
    {
        
    }
}

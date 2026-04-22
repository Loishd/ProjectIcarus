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

            //Player with Highest Score
            PlayerPrefs.SetString("MVPName", "SomChai");

            //Shop
            PlayerPrefs.SetInt("BoughtInvulnerability", 0);
            PlayerPrefs.SetInt("BoughtAttraction", 0);
            PlayerPrefs.SetInt("BoughtHeatShield", 0);

            PlayerPrefs.SetInt("CanEquipFlapModule", 0);
            PlayerPrefs.SetInt("CanEquipDiveModule", 0);
            PlayerPrefs.SetInt("CanEquipPlaneModule", 0);

            PlayerPrefs.SetInt("EquippedFlapModule", 0);
            PlayerPrefs.SetInt("EquippedDiveModule", 0);
            PlayerPrefs.SetInt("EquippedPlaneModule", 0);

            //Quests
            PlayerPrefs.SetInt("HoarderNextToPlutus", 0);
            PlayerPrefs.SetInt("IcarusArrogance", 0);
            PlayerPrefs.SetInt("VolatileFlight", 0);
            PlayerPrefs.SetInt("ZeusCantCatchMe", 0);
            PlayerPrefs.SetInt("SeekingForPoseidon", 0);
            PlayerPrefs.SetInt("AggressiveTyphoon", 0);

            //Misc.
            PlayerPrefs.SetInt("HasLaunchedBefore", 1);
            PlayerPrefs.Save();
        }
    }

    void Update()
    {
        
    }
}

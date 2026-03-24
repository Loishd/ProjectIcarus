using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saves : MonoBehaviour
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("HasLaunchedBefore"))
        {
            PlayerPrefs.SetFloat("HighestScore", 0);
            PlayerPrefs.SetFloat("CoinAmount", 0);

            PlayerPrefs.SetInt("HasLaunchedBefore", 1);
            PlayerPrefs.Save();
        }
    }

    void Update()
    {
        
    }
}

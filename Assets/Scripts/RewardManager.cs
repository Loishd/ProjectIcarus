using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] float TotalCoins;

    public void HoarderNextToPlutus()
    {
        if (PlayerPrefs.GetInt("HoarderNextToPlutus") == 1) //Total
        {
            Debug.Log("HoarderNextToPlutus Completed");
        }
    }

    public void IcarusArrogance()
    {
        if (PlayerPrefs.GetInt("IcarusArrogance") == 1) //Single Run
        {
            Debug.Log("IcarusArrogance Completed");
        }
    }

    public void VolatileFlight()
    {
        if (PlayerPrefs.GetInt("VolatileFlight") == 1) //Straight
        {
            Debug.Log("VolatileFlight Completed");
        }
    }

    public void ZeusCantCatchMe()
    {
        if (PlayerPrefs.GetInt("ZeusCantCatchMe") == 1) //Total
        {
            Debug.Log("ZeusCantCatchMe Completed");
        }
    }

    public void SeekingForPoseidon()    
    {
        if (PlayerPrefs.GetInt("SeekingForPoseidon") == 1) //Straight
        {
            Debug.Log("SeekingForPoseidon Completed");
        }
    }

    public void AggressiveTyphoon()
    {
        if (PlayerPrefs.GetInt("AggressiveTyphoon") >= 45) //Single Run
        {
            Debug.Log("AggressiveTyphoon Completed");
        }
    }
}

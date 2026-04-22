using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public PlayerMovement _playerReference;
    [SerializeField] TMP_Text gadgetText;
    RewardManager rewardManager;

    public bool isDeath = false;

    public bool isInvulnerability;
    public bool isMagnetic;
    public bool isHeatShield;

    public bool isFever;

    public int nearMissCount;
    public int touchWindCount;
    public int gadgetIndex;

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

    public void Update()
    {
        if (gadgetIndex == 0)
        {
            gadgetText.text = "Gadget: Null";
        }
        else
        {
            gadgetText.text = "Gadget: " + gadgetIndex;
        }
    }

    public void AddCoinToPlayer(float amount)
    {
        float overallCoin = PlayerPrefs.GetFloat("CoinAmount");

        PlayerPrefs.SetFloat("CoinAmount", amount + overallCoin);

        if ((PlayerPrefs.GetInt("HoarderNextToPlutus") != 1) && overallCoin >= 9999)
        {
            PlayerPrefs.SetInt("HoarderNextToPlutus", 1);
            StartCoroutine(RewardManager.Instance.PopUpQuest("Hoarder Next To Plutus"));
        }
    }

    public void TriggerNearMiss(float increaseScore)
    {
        ScoreManager.Instance._currentScore += increaseScore;
        nearMissCount += 1;

        if ((PlayerPrefs.GetInt("IcarusArrogance") != 1) && nearMissCount >= 15)
        {
            PlayerPrefs.SetInt("IcarusArrogance", 1);
            StartCoroutine(RewardManager.Instance.PopUpQuest("Icarus Arrogance"));
        }
    }


}

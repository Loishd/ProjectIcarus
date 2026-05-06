using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [Header("GadgetUI")]
    [SerializeField] List<Sprite> wingSprite = new List<Sprite>();
    [SerializeField] Image LeftWing;
    [SerializeField] Image RightWing;
    [SerializeField] RectTransform FeverGear;
    [SerializeField] List<RectTransform> FeverGearPos = new List<RectTransform>();

    [Header("ItemDuration")]
    [SerializeField] float _magnetDuration;
    [SerializeField] float _heatShieldDuration;
    [SerializeField] float _invulnerabilityDuration;
    [SerializeField] float _cloudDuration;

    public float MagnetDuration => _magnetDuration;
    public float HeatShieldDuration => _heatShieldDuration;
    public float InvulnerabilityDuration => _invulnerabilityDuration;

    public float CloudDuration => _cloudDuration;

    [Header("-------------")]
    public PlayerMovement _playerReference;
    [SerializeField] TMP_Text gadgetText;
    [SerializeField] GameObject nearMissVisual;
    public float MoveSpeedRef;
    public float speedIncrease;

    public bool isDeath = false;

    public bool isInvulnerability;
    public bool isMagnetic;
    public bool isHeatShield;
    public bool isCloud;

    public bool isFever;

    public float nearMissShowtime = 1f;
    public int nearMissCount;

    public int touchWindCount;
    public int gadgetIndex;

    SpriteRenderer spriteRenderer;

    [Header("Quest")]
    [SerializeField] int featherReach = 3000;

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

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        speedIncrease = 1;
    }

    public void Update()
    {
        if (ScoreManager.Instance.isPause) return;
        if (PlayerStatus.Instance.isDeath) return;
        GadgetVisual();
        speedIncrease += Time.deltaTime / 2000;
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

        if ((PlayerPrefs.GetInt("HoarderNextToPlutus") != 1) && overallCoin >= featherReach)
        {
            PlayerPrefs.SetInt("HoarderNextToPlutus", 1);
            StartCoroutine(RewardManager.Instance.PopUpQuest("Hoarder Next To Plutus"));
        }
    }

    public IEnumerator TriggerNearMiss(float increaseScore)
    {
        ScoreManager.Instance._currentScore += increaseScore;
        nearMissCount += 1;

        if ((PlayerPrefs.GetInt("IcarusArrogance") != 1) && nearMissCount >= 15)
        {
            PlayerPrefs.SetInt("IcarusArrogance", 1);
            StartCoroutine(RewardManager.Instance.PopUpQuest("Icarus Arrogance"));

            if (PlayerPrefs.GetInt("CanEquipFlapModule") != 1)
                PlayerPrefs.SetInt("CanEquipFlapModule", 1);
        }
        nearMissVisual.SetActive(true);
        yield return new WaitForSeconds(nearMissShowtime);
        nearMissVisual.SetActive(false);

        
    }
    public void GadgetVisual()
    {
        if (gadgetIndex == 0)
        {
            FeverGear.position = FeverGearPos[0].position;
            LeftWing.sprite = wingSprite[0];
            RightWing.sprite = wingSprite[1];
        }
        else if (gadgetIndex == 1)
        {
            FeverGear.position = FeverGearPos[1].position;
            LeftWing.sprite = wingSprite[2];
            RightWing.sprite = wingSprite[3];
        }
        else if (gadgetIndex == 2)
        {
            FeverGear.position = FeverGearPos[2].position;
            LeftWing.sprite = wingSprite[4];
            RightWing.sprite = wingSprite[5];
        }
        else if (gadgetIndex == 3)
        {
            FeverGear.position = FeverGearPos[3].position;
            LeftWing.sprite = wingSprite[6];
            RightWing.sprite = wingSprite[7];
        }
    }
}

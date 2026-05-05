using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] float TotalCoins;

    [SerializeField] private TMP_Text questNameText;
    [SerializeField] private GameObject questUI;
    [SerializeField] private float popUpDuration = 2f;
    [SerializeField] private float popUpSpeed = 2f;
    [SerializeField] private float distance = 20f;
    [SerializeField] GameObject targetpos;

    [SerializeField] private bool isDebugging = false;

    public static RewardManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) 
            Instance = this;
    }

    private void Update()
    {
        if (ScoreManager.Instance.isPause) return;
        if (!isDebugging) return;

        if (Input.GetKeyDown(KeyCode.U))
        {
            PlayerPrefs.SetInt("HoarderNextToPlutus", 0);
            PlayerPrefs.SetInt("IcarusArrogance", 0);
            PlayerPrefs.SetInt("VolatileFlight", 0);
            PlayerPrefs.SetInt("SeekingForPoseidon", 0);
            PlayerPrefs.SetInt("AggressiveTyphoon", 0);

            PlayerPrefs.SetInt("CanEquipFlapModule", 0);
            PlayerPrefs.SetInt("CanEquipDiveModule", 0);
            PlayerPrefs.SetInt("CanEquipPlaneModule", 0);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Plutus : " + PlayerPrefs.GetInt("HoarderNextToPlutus"));
            Debug.Log("IcarusArrogance : " + PlayerPrefs.GetInt("IcarusArrogance"));
            Debug.Log("Volatile Filight : " + PlayerPrefs.GetInt("VolatileFlight"));
            Debug.Log("Seeking Poseidon : " + PlayerPrefs.GetInt("SeekingForPoseidon"));
            Debug.Log("Typhoon : " + PlayerPrefs.GetInt("AggressiveTyphoon"));

        }
    }

    public IEnumerator PopUpQuest(string name)
    {
        questNameText.text = name;
        Vector3 defaultPos = targetpos.transform.position;
        Vector3 leftPos = defaultPos + Vector3.left * 500f;

        StartCoroutine(MoveObject(defaultPos, leftPos, 0.25f));

        yield return new WaitForSeconds(popUpDuration);

        StartCoroutine(MoveObject(leftPos, defaultPos, 0.25f));
    }

    IEnumerator MoveObject(Vector3 start, Vector3 end, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            questUI.transform.position = Vector3.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        questUI.transform.position = end;
    }

    public bool HoarderNextToPlutus()
    {
        if (PlayerPrefs.GetInt("HoarderNextToPlutus") == 1) //Total
            return true;
        else
            return false;
    }

    public bool IcarusArrogance()
    {
        if (PlayerPrefs.GetInt("IcarusArrogance") == 1) //Single Run
            return true;
        else
            return false;
    }

    public bool VolatileFlight()
    {
        if (PlayerPrefs.GetInt("VolatileFlight") == 1) //Straight
            return true;
        else
            return false;
    }

    public bool SeekingForPoseidon()    
    {
        if (PlayerPrefs.GetInt("SeekingForPoseidon") == 1) //Straight
            return true;
        else
            return false;
    }

    public bool AggressiveTyphoon()
    {
        if (PlayerPrefs.GetInt("AggressiveTyphoon") >= 45) //Single Run
            return true;
        else
            return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarterBuff : MonoBehaviour
{
    public float timeRemaining = 20;
    public bool timerIsRunning = false;
    public TMP_Text timeText;
    public GameObject menu;
    public int invulnerabilityAmount;
    public int attractionAmount;
    public int heatShieldAmount;    

    private void Start()
    {
        if (PlayerPrefs.GetInt("BoughtInvulnerability") == 0 && PlayerPrefs.GetInt("BoughtAttraction") == 0 && PlayerPrefs.GetInt("BoughtHeatShield") == 0) return;

        menu.SetActive(true);
        timerIsRunning = true;

        invulnerabilityAmount = PlayerPrefs.GetInt("BoughtInvulnerability");
        attractionAmount = PlayerPrefs.GetInt("BoughtAttraction");
        heatShieldAmount = PlayerPrefs.GetInt("BoughtHeatShield");

        StarterGadget();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                menu.SetActive(false);
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GetBuff(int itemIndex)
    {
        if (PlayerStatus.Instance.isDeath) return;

        if (itemIndex == 0 && PlayerPrefs.GetInt("BoughtInvulnerability") >= 1 && !PlayerStatus.Instance.isInvulnerability)
        {
            PlayerStatus.Instance.isInvulnerability = true;
            PlayerPrefs.SetInt("BoughtInvulnerability", invulnerabilityAmount - 1);
            Debug.Log(gameObject + "Used");
            menu.SetActive(false);
        }
            
        if (itemIndex == 1 && PlayerPrefs.GetInt("BoughtAttraction") >= 1 && !PlayerStatus.Instance.isMagnetic)
        {
            PlayerStatus.Instance.isMagnetic = true;
            PlayerPrefs.SetInt("BoughtAttraction", attractionAmount - 1);
            menu.SetActive(false);
        }
            
        if (itemIndex == 2 && PlayerPrefs.GetInt("BoughtHeatShield") >= 1 && !PlayerStatus.Instance.isHeatShield)
        {
            PlayerStatus.Instance.isHeatShield = true;
            PlayerPrefs.SetInt("BoughtHeatShield", heatShieldAmount - 1);
            menu.SetActive(false);
        }
    }

    void StarterGadget()
    {
        if (PlayerPrefs.GetInt("EquippedFlapModule") == 1)
        {
            PlayerStatus.Instance.gadgetIndex = 1;
        }
        
        if (PlayerPrefs.GetInt("EquippedDiveModule") == 1)
        {
            PlayerStatus.Instance.gadgetIndex = 2;
        }

        if (PlayerPrefs.GetInt("EquippedPlaneModule") == 1)
        {
            PlayerStatus.Instance.gadgetIndex = 3;
        }
    }
}

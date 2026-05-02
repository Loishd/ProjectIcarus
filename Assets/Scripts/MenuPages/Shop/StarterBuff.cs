using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarterBuff : MonoBehaviour
{
    public float timeRemaining = 20;
    public bool timerIsRunning = false;

    public TMP_Text timeText;

    public TMP_Text buff1Text;
    public TMP_Text buff2Text;
    public TMP_Text buff3Text;

    public Button buff1Button;
    public Button buff2Button;
    public Button buff3Button;

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

        buff1Text.text = invulnerabilityAmount.ToString();
        buff2Text.text = attractionAmount.ToString();
        buff3Text.text = heatShieldAmount.ToString();

        if (invulnerabilityAmount <= 0)
            buff1Button.interactable = false;

        if (attractionAmount <= 0)
            buff2Button.interactable = false;

        if (heatShieldAmount <= 0)
            buff3Button.interactable = false;

        StarterGadget();
    }

    void Update()
    {
        if (ScoreManager.Instance.isPause) return;
        if (PlayerStatus.Instance.isDeath) return;

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

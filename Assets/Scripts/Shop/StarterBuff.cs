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

    private void Start()
    {
        if (PlayerPrefs.GetInt("BoughtInvulnerability") == 0 && PlayerPrefs.GetInt("BoughtAttraction") == 0 && PlayerPrefs.GetInt("BoughtHeatShield") == 0) return;

        menu.SetActive(true);
        timerIsRunning = true;
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
        if (itemIndex == 0 && PlayerPrefs.GetInt("BoughtInvulnerability") == 1)
        {
            Debug.Log("Give Invulnerability!");
            PlayerStatus.Instance.isInvulnerability = true;
            PlayerPrefs.SetInt("BoughtInvulnerability", 0);
            menu.SetActive(false);
        }
            
        if (itemIndex == 1 && PlayerPrefs.GetInt("BoughtAttraction") == 1)
        {
            Debug.Log("BoughtAttraction!");
            PlayerStatus.Instance.isMagnetic = true;
            PlayerPrefs.SetInt("BoughtAttraction", 0);
            menu.SetActive(false);
        }
            
        if (itemIndex == 2 && PlayerPrefs.GetInt("BoughtHeatShield") == 1)
        {
            Debug.Log("BoughtHeatShield!");
            PlayerStatus.Instance.isHeatShield = true;
            PlayerPrefs.SetInt("BoughtHeatShield", 0);
            menu.SetActive(false);
        }
    }
}

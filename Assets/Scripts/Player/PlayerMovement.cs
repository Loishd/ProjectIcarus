using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float InvulnerabilityShieldTime;
    public bool isInvulnerability;
    public float moveSpeed = 5f;
    public int currentLane = 1;
    public float laneDistance = 5f;
    public float changeSpeed = 5f;
    public float speedIncrease = 1;
    [SerializeField] GameObject InvulnerabilityShield;
    [SerializeField] GameObject deathScreen;

    private void Start()
    {
        isInvulnerability = false;
        speedIncrease = 1;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) Death();
        InvulnerabilityVisual();
        LaneSwapper();
        speedIncrease += Time.deltaTime/2000;
        if (isInvulnerability)
        {
            StartCoroutine(InvulnerabilityTimer(InvulnerabilityShieldTime));
        }
    }

    void FixedUpdate()
    {
        AutoWalk();
    }

    void AutoWalk()
    {
        Vector3 movement = new Vector3(0f, 1f, 0f).normalized;

        transform.Translate(movement * moveSpeed * Time.deltaTime * speedIncrease);
    }

    void LaneSwapper()
    {
        //Check Lane
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentLane == 0) return;
            currentLane--;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentLane == 2) return;
            currentLane++;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (currentLane == 0) targetPosition += Vector3.left * laneDistance;
        else if (currentLane == 2) targetPosition += Vector3.right * laneDistance;

        transform.position = Vector3.Lerp(transform.position, targetPosition, changeSpeed * Time.deltaTime);
    }

    public void Death()
    {
        SetHighestScore();
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    void SetHighestScore()
    {
        if (ScoreManager.Instance._currentScore > PlayerPrefs.GetFloat("HighestScore"))
        {
            PlayerPrefs.SetFloat("HighestScore", ScoreManager.Instance._currentScore);
        }
    }

    void InvulnerabilityVisual()
    {
        if (isInvulnerability)
        {
            InvulnerabilityShield.SetActive(true);
        }
        else
        {
            InvulnerabilityShield.SetActive(false);
        }
    }

    IEnumerator InvulnerabilityTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        isInvulnerability = false;
        ScoreManager.Instance.InvulnerabilityMultiplier = 2;
    }
}

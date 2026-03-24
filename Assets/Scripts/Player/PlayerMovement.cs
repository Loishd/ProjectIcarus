using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Buff Time")]
    public float InvulnerabilityShieldTime;
    public float MagnetTimer;

    [Header("Movement Variable")]
    public float moveSpeed = 5f;
    public int currentLane = 1;
    public float laneDistance = 5f;
    public float changeSpeed = 5f;
    public float speedIncrease = 1;

    [Header("Buff Collider")]
    [SerializeField] GameObject InvulnerabilityShield;
    [SerializeField] GameObject MagnetCollider;

    [SerializeField] GameObject deathScreen;

    private void Start()
    {
        PlayerStatus.Instance.isInvulnerability = false;
        speedIncrease = 1;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) Death();
        InvulnerabilityVisual();
        MagnetVisual();
        LaneSwapper();
        speedIncrease += Time.deltaTime/2000;

        //------------------------------//

        if (PlayerStatus.Instance.isInvulnerability)
        {
            StartCoroutine(InvulnerabilityTimer(InvulnerabilityShieldTime));
        }
        else if (PlayerStatus.Instance.isMagnetic)
        {
            StartCoroutine(MagnetTime(MagnetTimer));
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
        AddCoinToPlayer();
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

    void AddCoinToPlayer()
    {
        float overallCoin = PlayerPrefs.GetFloat("CoinAmount");

        PlayerPrefs.SetFloat("CoinAmount", ScoreManager.Instance.CurrentCoins + overallCoin);
    }

    void InvulnerabilityVisual()
    {
        if (PlayerStatus.Instance.isInvulnerability)
        {
            InvulnerabilityShield.SetActive(true);
        }
        else
        {
            InvulnerabilityShield.SetActive(false);
        }
    }

    void MagnetVisual()
    {
        if (PlayerStatus.Instance.isMagnetic)
        {
            MagnetCollider.SetActive(true);
        }
        else
        {
            MagnetCollider.SetActive(false);
        }
    }

    IEnumerator InvulnerabilityTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        PlayerStatus.Instance.isInvulnerability = false;
        ScoreManager.Instance.InvulnerabilityMultiplier = 2;
    }

    IEnumerator MagnetTime(float timer)
    {
        yield return new WaitForSeconds(timer);
        PlayerStatus.Instance.isMagnetic = false;
    }
}

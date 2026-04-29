using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Variable")]
    public float moveSpeed = 5f;
    public int currentLane = 1;
    public float laneDistance = 5f;
    public float changeSpeed = 5f;
    public float speedIncrease = 1;

    [Header("Buff Collider")]
    [SerializeField] GameObject InvulnerabilityShield;
    [SerializeField] GameObject MagnetCollider;
    [SerializeField] GameObject HeatShieldCollider;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject cloudBlocker;

    [SerializeField] bool isOnSkipping;
    [SerializeField] MapSpawner mapSpawner;
    [SerializeField] StarterBuff starterBuff;
    [SerializeField] HeightSystem heightSystem;

    [Header("Debug Mode")]
    public bool godMode = false;
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer rb2dSprite;

    [Header("ItemTimer")]
    [SerializeField] float attractionTimer;
    [SerializeField] float heatShieldTimer;
    [SerializeField] float invulnerabilityTimer;
    [SerializeField] float cloudTime;

    public float InvulnerabilityTimer => invulnerabilityTimer;

    

    bool hasEnteredSkipper = false;
    bool hasExitedSkipper = false;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        PlayerStatus.Instance.isInvulnerability = false;
        speedIncrease = 1;
    }
    void Update()
    {
        if ((ScoreManager.Instance.isPause) || (PlayerStatus.Instance.isDeath)) return;
        SetPlayerHotColdNormalStatus();
        //----VISUAL----// 
        CloudVisual();
        InvulnerabilityVisual();
        MagnetVisual();
        HeatShieldVisual();
        LaneSwapper();

        //------------------------------//

        if (PlayerStatus.Instance.isInvulnerability)
        {
            InvulnerabilityTime(PlayerStatus.Instance.InvulnerabilityDuration);
        }

        if (PlayerStatus.Instance.isMagnetic)
        {
            MagnetTime(PlayerStatus.Instance.MagnetDuration);
        }

        if (PlayerStatus.Instance.isHeatShield)
        {
            HeatShieldTimer(PlayerStatus.Instance.HeatShieldDuration);
        }

        if (PlayerStatus.Instance.isCloud)
        {
            CloudTime(PlayerStatus.Instance.CloudDuration);
        }
    }

    void FixedUpdate()
    {
        //AutoWalk();
    }

    void AutoWalk()
    {
        Vector3 movement = new Vector3(0f, 1f, 0f).normalized;

        transform.Translate(movement * moveSpeed * Time.deltaTime * speedIncrease);
    }

    void LaneSwapper()
    {
        if (Time.timeScale == 0f) return;
        if (PlayerStatus.Instance.gadgetIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (currentLane == 2) return;
                animator.SetTrigger("MoveRight");
                SoundManager.Instance.PlaySFX(SoundManager.Instance.dashSfx);
                currentLane++;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (currentLane == 0) return;
                animator.SetTrigger("MoveLeft");
                SoundManager.Instance.PlaySFX(SoundManager.Instance.dashSfx);
                currentLane--;
            }

            Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

            if (currentLane == 0) targetPosition += Vector3.left * laneDistance;
            else if (currentLane == 2) targetPosition += Vector3.right * laneDistance;

            transform.position = Vector3.Lerp(transform.position, targetPosition, changeSpeed * Time.deltaTime);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (currentLane == 0) return;
                animator.SetTrigger("MoveLeft");
                SoundManager.Instance.PlaySFX(SoundManager.Instance.dashSfx);
                currentLane--;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (currentLane == 2) return;
                animator.SetTrigger("MoveRight");
                SoundManager.Instance.PlaySFX(SoundManager.Instance.dashSfx);
                currentLane++;
            }

            Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

            if (currentLane == 0) targetPosition += Vector3.left * laneDistance;
            else if (currentLane == 2) targetPosition += Vector3.right * laneDistance;

            transform.position = Vector3.Lerp(transform.position, targetPosition, changeSpeed * Time.deltaTime);
        }
    }

    public void Death()
    {
        if (godMode) return;

        SetHighestScore();
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
        PlayerStatus.Instance.isDeath = true;
        PlayerStatus.Instance.nearMissCount = 0;
        PlayerStatus.Instance.gadgetIndex = 0;
        starterBuff.menu.SetActive(false);
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

    void HeatShieldVisual()
    {
        if (PlayerStatus.Instance.isHeatShield)
            HeatShieldCollider.SetActive(true);
        else
            HeatShieldCollider.SetActive(false);
            
    }

    void CloudVisual()
    {
        if (PlayerStatus.Instance.isCloud)
           cloudBlocker.SetActive(true);
        else
            cloudBlocker.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Skipper") && !hasEnteredSkipper)
        {
            hasEnteredSkipper = true;
            hasExitedSkipper = false;

            Debug.Log("ENTER ONCE: " + collision.name);

            mapSpawner.ChangeMapPosition();
            return;
        }

        if (collision.CompareTag("Cloud"))
        {
            if (PlayerStatus.Instance.isCloud)
            {
                ExtendCloud();
            }
            else
            {
                PlayerStatus.Instance.isCloud = true;
            }
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Skipper") && !hasExitedSkipper)
        {
            hasExitedSkipper = true;
            hasEnteredSkipper = false;

            Debug.Log("EXIT ONCE: " + collision.name);

            mapSpawner.ResetPattern();
        }
    }

    public void InvulnerabilityTime(float duration)
    {
        invulnerabilityTimer += Time.deltaTime;
        if (invulnerabilityTimer >= duration)
        {
            invulnerabilityTimer = 0;
            PlayerStatus.Instance.isInvulnerability = false;
        }
    }

    public void MagnetTime(float duration)
    {
        attractionTimer += Time.deltaTime;
        if (attractionTimer >= duration)
        {
            attractionTimer = 0;
            PlayerStatus.Instance.isMagnetic = false;
        }
    }

    public void HeatShieldTimer(float duration)
    {
        heatShieldTimer += Time.deltaTime;
        if (heatShieldTimer >= duration)
        {
            heatShieldTimer = 0;
            PlayerStatus.Instance.isHeatShield = false;
        }
    }

    public void CloudTime(float duration)
    {
        cloudTime += Time.deltaTime;
        if (cloudTime >= duration)
        {
            cloudTime = 0;
            PlayerStatus.Instance.isCloud = false;
        }
    }

    IEnumerator DeathTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        Death();
    }

    public void SetDeathAnimation()
    {
        PlayerStatus.Instance.isDeath = true;
        moveSpeed = 0;
        animator.SetTrigger("DeadAnimation");
        StartCoroutine(DeathTimer(2));
    }

    public void SetPlayerHotColdNormalStatus()
    {
        animator.SetFloat("Height", heightSystem.CurrentHeight);
    }

    public void ResetInvulnerabilityTime()
    {
        invulnerabilityTimer = 0;
    }

    public void ExtendItemInvulnerability()
    {
        invulnerabilityTimer -= PlayerStatus.Instance.InvulnerabilityDuration;
    }

    public void ExtendItemAttraction()
    {
        attractionTimer -= PlayerStatus.Instance.MagnetDuration;
    }

    public void ExtendItemHeatShield()
    {
        heatShieldTimer -= PlayerStatus.Instance.HeatShieldDuration;
    }

    public void ExtendCloud()
    {
        cloudTime -= PlayerStatus.Instance.CloudDuration;
    }
}

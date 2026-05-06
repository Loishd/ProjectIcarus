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
    [SerializeField] DeathUIScript deathUIscript;

    [Header("ItemTimer")]
    [SerializeField] float attractionTimer;
    [SerializeField] float heatShieldTimer;
    [SerializeField] float invulnerabilityTimer;
    [SerializeField] float cloudTime;

    [Header("UI Bar Settings")]
    [SerializeField] GameObject barPrefab;
    [SerializeField] Transform barContainer;

    [Header("Item Icons")]
    public Sprite magnetIcon;
    public Sprite shieldIcon;
    public Sprite invulIcon;

    // ตัวแปรเก็บอ้างอิงบาร์ที่กำลังแสดงผล (เหลือแค่ 3 อันหลัก)
    private ItemUI activeMagnetBar;
    private ItemUI activeShieldBar;
    private ItemUI activeInvulBar;

    public float InvulnerabilityTimer => invulnerabilityTimer;

    

    //bool hasEnteredSkipper = false;
    //bool hasExitedSkipper = false;

    private Collider2D currentSkipper;
    [SerializeField] GameObject UItexts;


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

        SoundManager.Instance.PlaySFX(SoundManager.Instance.deathSfx);
        SetHighestScore();
        UItexts.SetActive(false);
        deathScreen.SetActive(true);
        deathUIscript.UpdateDeathUIText();
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
    public void RemoveInvulBar()
    {
        if (activeInvulBar != null)
        {
            Destroy(activeInvulBar.gameObject);
            activeInvulBar = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Skipper") && currentSkipper != collision)
        {
            currentSkipper = collision;

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

        if (collision.CompareTag("Bird"))
        {
            RemoveInvulBar();
        }

        if (collision.CompareTag("Magnet"))
        {
            PlayerStatus.Instance.isMagnetic = true;
            // attractionTimer = 0; // ใส่ตัวนับเวลาของพี่ตรงนี้

            if (activeMagnetBar != null)
            {
                activeMagnetBar.ResetTimer();
            }
            else
            {
                GameObject go = Instantiate(barPrefab, barContainer);
                // บรรทัดนี้จะไม่แดงแล้ว เพราะชื่อคลาสข้างบนคือ ItemUI
                activeMagnetBar = go.GetComponent<ItemUI>();
                activeMagnetBar.Setup(PlayerStatus.Instance.MagnetDuration, magnetIcon);
            }
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("HeatShield"))
        {
            PlayerStatus.Instance.isHeatShield = true;
            // heatShieldTimer = 0; // รีเซ็ตเวลาใน Player script ของพี่

            if (activeShieldBar != null)
            {
                activeShieldBar.ResetTimer(); // บาร์เดิมที่มีอยู่จะเด้งกลับมาเต็ม
            }
            else
            {
                GameObject ItemUIObj = Instantiate(barPrefab, barContainer);
                activeShieldBar = ItemUIObj.GetComponent<ItemUI>();
                activeShieldBar.Setup(PlayerStatus.Instance.HeatShieldDuration, shieldIcon);
            }
            Destroy(collision.gameObject);
        }

        // --- 3. INVULNERABILITY (อมตะ) ---
        else if (collision.CompareTag("Invulnerability"))
        {
            PlayerStatus.Instance.isInvulnerability = true;
            // invulnerabilityTimer = 0; // รีเซ็ตเวลาใน Player script ของพี่

            if (activeInvulBar != null)
            {
                activeInvulBar.ResetTimer(); // บาร์เดิมที่มีอยู่จะเด้งกลับมาเต็ม
            }
            else
            {
                GameObject ItemUIObj = Instantiate(barPrefab, barContainer);
                activeInvulBar = ItemUIObj.GetComponent<ItemUI>();
                activeInvulBar.Setup(PlayerStatus.Instance.InvulnerabilityDuration, invulIcon);
            }
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Skipper") && currentSkipper == collision)
        {
            Debug.Log("EXIT ONCE: " + collision.name);

            currentSkipper = null;
            mapSpawner.ResetPattern();
        }
    }

    public void SpawnMagnetBarExternal()
    {
        if (activeMagnetBar != null) activeMagnetBar.ResetTimer();
        else
        {
            GameObject ItemUI = Instantiate(barPrefab, barContainer);
            activeMagnetBar = ItemUI.GetComponent<ItemUI>();
            activeMagnetBar.Setup(PlayerStatus.Instance.MagnetDuration, magnetIcon);
        }
    }

    public void SpawnShieldBarExternal()
    {
        if (activeShieldBar != null) activeShieldBar.ResetTimer();
        else
        {
            GameObject ItemUI = Instantiate(barPrefab, barContainer);
            activeShieldBar = ItemUI.GetComponent<ItemUI>();
            activeShieldBar.Setup(PlayerStatus.Instance.HeatShieldDuration, shieldIcon);
        }
    }

    public void SpawnInvulBarExternal()
    {
        if (activeInvulBar != null) activeInvulBar.ResetTimer();
        else
        {
            GameObject ItemUI = Instantiate(barPrefab, barContainer);
            activeInvulBar = ItemUI.GetComponent<ItemUI>();
            activeInvulBar.Setup(PlayerStatus.Instance.InvulnerabilityDuration, invulIcon);
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
        invulnerabilityTimer = 0;
    }

    public void ExtendItemAttraction()
    {
        attractionTimer = 0;
    }

    public void ExtendItemHeatShield()
    {
        heatShieldTimer = 0;
    }

    public void ExtendCloud()
    {
        cloudTime = 0;
    }
}

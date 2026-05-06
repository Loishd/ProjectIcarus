using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightSystem : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] SpriteRenderer background;
    [SerializeField] private float currentHeight = 50f;
    [SerializeField] Color feverColor;
    [SerializeField] Color deathColor;
    public float CurrentHeight => currentHeight;
    [SerializeField] private float increaseSpeed = 3f;
    [SerializeField] private float decreaseSpeed = 1f;
    [SerializeField] private float gadgetIncreaseAmount;
    [SerializeField] float fallSpeed;
    private float maxHeight = 100f;
    private float minHeight = 0f;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float rainbowSpeed = 2f;
    [SerializeField] private float descentDecreaseSpeed;
    [SerializeField] float rainbowAlpha;
    [SerializeField] GameObject feverVisual;
    [SerializeField] private float fadeDuration = 0.5f;

    [Header("Sound Trigger Settings")]
    [SerializeField] private float hotThreshold = 70f;
    [SerializeField] private float coldThreshold = 30f;
    private bool hasPlayedHot = false;
    private bool hasPlayedCold = false;

    Color normalColor = Color.white;
    Color redColor = Color.red;
    Color blueColor = Color.blue;

    [SerializeField] PlayerStatus playerStatus;

    private void Start()
    {
        currentHeight = 50f;
        hasPlayedHot = false;
        hasPlayedCold = false;

        // สั่งหยุดเสียงที่อาจจะค้างมาจากรอบที่แล้ว
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ResetAllSounds();
        }
    }

    void Update()
    {
        if (ScoreManager.Instance.isPause) return;
        Gadget2();
        HeightManager();
        BackgroundColor();
        HeightVisual();
        CheckHeightSound();
    }

    void CheckHeightSound()
    {
        if (PlayerStatus.Instance.isDeath) return;
        // --- โซนร้อน (70+) ---
        if (currentHeight >= 70f)
        {
            if (!hasPlayedHot)
            {
                SoundManager.Instance.PlayWarningFade(SoundManager.Instance.hotWingSfx, fadeDuration);
                hasPlayedHot = true;
                hasPlayedCold = false;
            }
        }

        // --- โซนหนาว (30-) ---
        else if (currentHeight <= 30f)
        {
            if (!hasPlayedCold)
            {
                SoundManager.Instance.PlayWarningFade(SoundManager.Instance.wetWingSfx, fadeDuration);
                hasPlayedCold = true;
                hasPlayedHot = false;
            }
        }

        // --- โซนปกติ (31-69): ค่อยๆ เบาเสียงจนดับ ---
        else
        {
            if (hasPlayedHot || hasPlayedCold)
            {
                SoundManager.Instance.StopWarningFade(fadeDuration);
                hasPlayedHot = false;
                hasPlayedCold = false;
            }
        }
    }

    void HeightManager()
    {
        if (PlayerStatus.Instance.gadgetIndex == 3)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (currentHeight <= minHeight)
                {
                    currentHeight = minHeight;
                }
                else
                {
                    currentHeight -= decreaseSpeed * Time.deltaTime;
                }
            }
            else
            {
                currentHeight += increaseSpeed * Time.deltaTime;
            }
        }
        else if (PlayerStatus.Instance.gadgetIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IncreaseHeight(gadgetIncreaseAmount);
            }
            else
            {
                if (currentHeight <= minHeight)
                {
                    currentHeight = minHeight;
                }
                else
                {
                    currentHeight -= decreaseSpeed * Time.deltaTime;
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                currentHeight += increaseSpeed * Time.deltaTime;

            }
            else
            {
                if (currentHeight <= minHeight)
                {
                    currentHeight = minHeight;
                }
                else
                {
                    currentHeight -= decreaseSpeed * Time.deltaTime;
                }
            }
        }

    }

    void HeightVisual()
    {
        if ((currentHeight >= 100f && !PlayerStatus.Instance.isHeatShield) || currentHeight <= 0f)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.StopWarningImmediate();
            }
            player.Death();
        }

        // ส่วนเปลี่ยนสี Sprite ตัวละคร (คงไว้เหมือนเดิม)
        if (PlayerStatus.Instance.isDeath)
        {
            sprite.color = deathColor;
        }
        else
        {
            sprite.color = normalColor;
        }
    }

    void BackgroundColor()
    {
        Color start = new Color(0f, 0f, 1f, 0.3f); // blue, low alpha
        Color end = new Color(1f, 0f, 0f, 0.3f); // red, same low alpha

        background.color = Color.Lerp(start, end, currentHeight / maxHeight);
    }

    public void DecreaseHeight(float decreaseAmount)
    {
        currentHeight -= decreaseAmount;
    }

    public void IncreaseHeight(float increaseAmount)
    {
        currentHeight += increaseAmount;
    }

    public void Gadget2()
    {
        if (PlayerStatus.Instance.gadgetIndex == 2)
        {
            if ((Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.DownArrow)))
            {
                decreaseSpeed += Time.deltaTime * fallSpeed ;
            }
            else
            {
                if (decreaseSpeed < 6)
                {
                    decreaseSpeed = 6;
                }
                decreaseSpeed -= Time.deltaTime * descentDecreaseSpeed;
            }
        }
    }

    public void FreezeHeight(float Height)
    {
        currentHeight = Height;
    }
}

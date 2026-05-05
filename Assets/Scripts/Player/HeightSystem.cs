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

    Color normalColor = Color.white;
    Color redColor = Color.red;
    Color blueColor = Color.blue;

    [SerializeField] PlayerStatus playerStatus;

    void Update()
    {
        if (ScoreManager.Instance.isPause) return;
        Gadget2();
        HeightManager();
        BackgroundColor();
        HeightVisual();
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
        if (currentHeight >= 100f && !PlayerStatus.Instance.isHeatShield)
        {
            
            player.Death();
        }

        if (currentHeight <= 0f)
        {
            
            player.Death();
        }

        if (PlayerStatus.Instance.isFever)
        {
            float hue = (Time.time * rainbowSpeed) % 1.0f;
            Color rainbowColor = Color.HSVToRGB(hue, 0.8f, 1f);
            rainbowColor.a = rainbowAlpha;
            sprite.color = rainbowColor;
        }
        else if (PlayerStatus.Instance.isDeath)
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

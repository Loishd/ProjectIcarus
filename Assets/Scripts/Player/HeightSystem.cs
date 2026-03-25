using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightSystem : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] SpriteRenderer background;
    [SerializeField] private float currentHeight = 50f;
    public float CurrentHeight => currentHeight;
    [SerializeField] private float increaseSpeed = 3f;
    [SerializeField] private float decreaseSpeed = 1f;
    private float dangerHeatZone = 100f;
    private float dangerFreezeZone = 0f;
    private float maxHeight = 100f;
    private float minHeight = 0f;
    [SerializeField] private SpriteRenderer sprite;

    Color normalColor = Color.white;
    Color redColor = Color.red;
    Color blueColor = Color.blue;

    [SerializeField] PlayerStatus playerStatus;

    void Update()
    {
        HeightManager();
        BackgroundColor();
        HeightVisual();
    }
    
    void HeightManager()
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

    void HeightVisual()
    {

        //------------------------------ Heat Zone :fire: -------------------------------------
        if (currentHeight >= dangerHeatZone)
        {
            if (playerStatus.isDeath) return;

            Destroy(gameObject);
            Debug.Log("You Death By Heat.");
        }

        //--------------------------- Freeze Zone :ice: b------------------------------------
        if (currentHeight <= dangerFreezeZone)
        {
            if (playerStatus.isDeath) return;

            Destroy(gameObject);
            Debug.Log("You Death By Freeze/Falling.");
        }

        //--------------------------- Slowly Turn Red ----------------------------------------
        if (currentHeight >= 100f && !PlayerStatus.Instance.isHeatShield)
        {
            float t = Mathf.InverseLerp(30f, maxHeight, currentHeight);

            sprite.color = Color.Lerp(normalColor, redColor, t);
            player.Death();
        }
        else
        {
            sprite.color = normalColor;
        }

        //--------------------------- Slowly Turn Blue ----------------------------------------
        if (currentHeight <= 0f)
        {
            float t = Mathf.InverseLerp(-30f, minHeight, currentHeight);

            sprite.color = Color.Lerp(normalColor, blueColor, t);
            player.Death();
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
}

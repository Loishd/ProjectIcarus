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
            if (currentHeight >= maxHeight)
            {
                currentHeight = maxHeight;
            }
            else
            {
                currentHeight += increaseSpeed * Time.deltaTime;
            }
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
        //If dangerZone = 90, so it is 80-89.
        float HeatZoneMax = 90;
        float HeatZoneMin = 10f;

        //------------------------------ Heat Zone :fire: -------------------------------------
        if (currentHeight >= dangerHeatZone)
        {
            if (playerStatus.isDeath) return;

            Destroy(gameObject);
            Debug.Log("You Death By Heat.");
        }
        else if (currentHeight >= HeatZoneMin && currentHeight <= HeatZoneMax)
        {
            //Fever Increase Here
            if (playerStatus.feverScore > 100) return;
            playerStatus.feverScore += Time.deltaTime;
        }

        //--------------------------- Freeze Zone :ice: b------------------------------------
        if (currentHeight <= dangerFreezeZone)
        {
            if (playerStatus.isDeath) return;

            Destroy(gameObject);
            Debug.Log("You Death By Freeze/Falling.");
        }

        //--------------------------- Slowly Turn Red ----------------------------------------
        if (currentHeight >= 100f)
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
        background.color = Color.Lerp(Color.blue, Color.red, (currentHeight/maxHeight));
    }
}

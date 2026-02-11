using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightSystem : MonoBehaviour
{
    [SerializeField] private float currentHeight = 50f;
    [SerializeField] private float increaseSpeed = 3f;
    [SerializeField] private float decreaseSpeed = 1f;
    private float dangerHeatZone = 90f;
    private float dangerFreezeZone = 10;
    private float maxHeight = 100f;
    private float minHeight = 0f;
    [SerializeField] private SpriteRenderer sprite;

    Color normalColor = Color.white;
    Color redColor = Color.red;
    Color blueColor = Color.blue;

    PlayerStatus playerStatus;

    void Update()
    {
        HeightManager();
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
        float HeatZoneMax = dangerHeatZone - 1f;
        float HeatZoneMin = dangerHeatZone - 10f;

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
        if (currentHeight >= 70f)
        {
            float t = Mathf.InverseLerp(70f, maxHeight, currentHeight);

            sprite.color = Color.Lerp(normalColor, redColor, t);
        }
        else
        {
            sprite.color = normalColor;
        }

        //--------------------------- Slowly Turn Blue ----------------------------------------
        if (currentHeight <= 30f)
        {
            float t = Mathf.InverseLerp(30f, minHeight, currentHeight);

            sprite.color = Color.Lerp(normalColor, blueColor, t);
        }
        else
        {
            sprite.color = normalColor;
        }





    }

}

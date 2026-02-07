using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightSystem : MonoBehaviour
{
    [SerializeField] private float currentHeight = 50f;
    [SerializeField] private float increaseSpeed = 3f;
    [SerializeField] private float decreaseSpeed = 1f;
    private float maxHeight = 100f;
    private float minHeight = 0f;

    void Update()
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
}

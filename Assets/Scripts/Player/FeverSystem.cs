using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverSystem : MonoBehaviour
{
    public float feverMeter;

    void Start()
    {
            
    }

    void Update()
    {
        
    }

    public void IncreaseFever(float feverGain)
    {
        feverMeter += feverGain;
    }
}

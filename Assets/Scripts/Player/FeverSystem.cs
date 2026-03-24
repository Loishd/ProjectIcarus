using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverSystem : MonoBehaviour
{
    [SerializeField] private float feverMeter;
    [SerializeField] private float feverDuration = 10f;
    [SerializeField] private float feverMultiplier = 2f;
    private float feverMeterMax = 100f;
    private float feverMeterMin = 0f;

    void Start()
    {
        feverMeter = feverMeterMin;
    }

    void Update()
    {
        if (feverMeter >= feverMeterMax)
            StartCoroutine(ActiveFever());
        
    }

    public void IncreaseFever(float feverGain)
    {
        if (feverMeter > feverMeterMax)
            feverMeter = feverMeterMax;

        feverMeter += feverGain;
    }

    private IEnumerator ActiveFever()
    {
        PlayerStatus.Instance.isInvulnerability = true;
        ScoreManager.Instance.multiplier += feverMultiplier - 1;
        yield return new WaitForSeconds(feverDuration);
        feverMeter = feverMeterMin;
        PlayerStatus.Instance.isInvulnerability = false;
        ScoreManager.Instance.multiplier -= feverMultiplier - 1;
    }
}

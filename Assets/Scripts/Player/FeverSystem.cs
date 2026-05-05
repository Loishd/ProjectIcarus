using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverSystem : MonoBehaviour
{
    [SerializeField] private float feverMeter;
    [SerializeField] private float feverDuration = 10f;
    [SerializeField] private float feverMultiplier;
    public float FeverMultiplier => feverMultiplier;

    private float useFeverMultiplier;
    public float UseFeverMultiplier => useFeverMultiplier;
    private float feverMeterMax = 100f;
    private float feverMeterMin = 0f;
    public List<GameObject> feverBarList = new List<GameObject>();

    void Start()
    {
        feverMeter = feverMeterMin;
    }

    void Update()
    {
        if (ScoreManager.Instance.isPause) return;
        if (feverMeter >= feverMeterMax && !PlayerStatus.Instance.isFever)
            StartCoroutine(ActiveFever());
        
        if (PlayerStatus.Instance.isFever)
        {
            useFeverMultiplier = feverMultiplier;
        }
        else
        {
            useFeverMultiplier = 1;
        }
    }

    public void IncreaseFever(float feverGain)
    {
        if (feverMeter > feverMeterMax)
            feverMeter = feverMeterMax;

        feverMeter += feverGain;

        if (feverMeter >= 16)
            feverBarList[0].SetActive(true);

        if (feverMeter >= 33)
            feverBarList[1].SetActive(true);

        if (feverMeter >= 50)
            feverBarList[2].SetActive(true);

        if (feverMeter >= 66)
            feverBarList[3].SetActive(true);

        if (feverMeter >= 83)
            feverBarList[4].SetActive(true);

        if (feverMeter >= 100)
            feverBarList[5].SetActive(true);
    }

    private IEnumerator ActiveFever()
    {
        PlayerStatus.Instance.isFever = true;
        ScoreManager.Instance.multiplier += feverMultiplier;
        yield return new WaitForSeconds(feverDuration);
        feverMeter = feverMeterMin;
        PlayerStatus.Instance.isFever = false;
        ScoreManager.Instance.multiplier -= feverMultiplier;
        
        foreach (var fever in feverBarList)
            fever.SetActive(false);
    }
}

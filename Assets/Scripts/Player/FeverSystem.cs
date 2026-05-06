using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverSystem : MonoBehaviour
{
    [SerializeField] SpriteRenderer feverVisualSprite;
    [SerializeField] private float feverMeter;
    [SerializeField] private float feverDuration = 10f;
    [SerializeField] private float feverMultiplier;
    public float FeverMultiplier => feverMultiplier;

    private float useFeverMultiplier;
    public float UseFeverMultiplier => useFeverMultiplier;
    private float feverMeterMax = 100f;
    private float feverMeterMin = 0f;
    public List<GameObject> feverBarList = new List<GameObject>();

    [SerializeField] float timer;

    void Start()
    {
        feverVisualSprite.gameObject.SetActive(false);
        feverMeter = feverMeterMin;
    }

    void Update()
    {
        if (ScoreManager.Instance.isPause) return;
        ActiveFever(); FeverAlpha();
        if (feverMeter >= feverMeterMax && !PlayerStatus.Instance.isFever)
            PlayerStatus.Instance.isFever = true;
        
        if (PlayerStatus.Instance.isFever)
        {
            useFeverMultiplier = feverMultiplier;
        }
        else
        {
            useFeverMultiplier = 1;
        }
    }

    public void FeverAlpha()
    {
        if (PlayerStatus.Instance.isFever && feverVisualSprite != null)
        {
            float t = Mathf.Clamp01(timer / feverDuration);

            // ดึงค่าสีปัจจุบันมา เพื่อเอาค่า RGB เดิมไว้ เปลี่ยนแค่ Alpha
            Color startColor = new Color(feverVisualSprite.color.r, feverVisualSprite.color.g, feverVisualSprite.color.b, 1f);
            Color endColor = new Color(feverVisualSprite.color.r, feverVisualSprite.color.g, feverVisualSprite.color.b, 0f);

            // ใช้ Color.Lerp ค่อยๆ จางลงตามค่า t
            feverVisualSprite.color = Color.Lerp(startColor, endColor, t);
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

    public void ActiveFever()
    {
        if (PlayerStatus.Instance.isFever)
        {
            // เปิดไว้ตลอดช่วงที่ Fever
            if (!feverVisualSprite.gameObject.activeSelf)
                feverVisualSprite.gameObject.SetActive(true);

            timer += Time.deltaTime;

            // ... โค้ดส่วน multiplier ...

            if (timer >= feverDuration)
            {
                // จบ Fever
                PlayerStatus.Instance.isFever = false;
                feverMeter = feverMeterMin;
                timer = 0;

                // ปิด Object หลังจากจบ Fever (หรือจะให้ค้างไว้ที่ Alpha 0 ก็ได้)
                feverVisualSprite.gameObject.SetActive(false);

                foreach (var fever in feverBarList) fever.SetActive(false);
            }
        }
    }
}

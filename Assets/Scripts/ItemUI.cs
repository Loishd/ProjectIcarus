using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image fillImage; // รูปแถบสีเขียว (Filled)
    public Image iconImage; // รูปไอคอนสีขาวในบาร์

    private float duration;
    private float timer;

    public void Setup(float time, Sprite icon)
    {
        duration = time;
        timer = time; // เริ่มที่เวลาเต็ม
        if (icon != null) iconImage.sprite = icon;
    }

    // ฟังก์ชันสำหรับรีเซ็ตหลอดให้กลับมาเต็ม (เรียกใช้ตอนเก็บไอเทมซ้ำ)
    public void ResetTimer()
    {
        timer = duration;
    }

    void Update()
    {
        if (ScoreManager.Instance.isPause || PlayerStatus.Instance.isDeath)
        {
            return; // จบการทำงานของ Update ในเฟรมนี้ บาร์จะไม่ลด
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime; // ค่อยๆ ลบเวลาออก
            fillImage.fillAmount = timer / duration; // หลอดจะหดลง
        }
        else
        {
            Destroy(gameObject); // เวลาหมดก็ทำลายตัวเอง
        }
    }
}

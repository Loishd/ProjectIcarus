using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSound : MonoBehaviour
{
    private bool hasPlayed = false; // กันมันร้องซ้ำหลายรอบในจอเดียว

    // ฟังก์ชันนี้ Unity จะเรียกให้เองเมื่อนก "โผล่เข้ามาในจอ"
    private void OnBecameVisible()
    {
        if (!hasPlayed)
        {
            // สั่งเล่นเสียงนกผ่าน SoundManager
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlaySFX(SoundManager.Instance.birdSfx);
            }
            hasPlayed = true; // ล็อคไว้ว่าร้องแล้วนะ
        }
    }

    // (เพิ่มเติม) ถ้าอยากให้นกร้องใหม่ได้เมื่อมันออกไปแล้วกลับเข้ามาใหม่
    private void OnBecameInvisible()
    {
        hasPlayed = false;
    }
}

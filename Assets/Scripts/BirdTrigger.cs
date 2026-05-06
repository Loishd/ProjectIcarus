using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdTrigger : MonoBehaviour
{
    private float lastBirdTime;
    public float birdCooldown = 1.0f; // ตั้งเวลาให้ยาวพอที่นกทั้งฝูงจะผ่านไป

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bird"))
        {
            Debug.Log(collision.name);
            if (Time.time >= lastBirdTime + birdCooldown)
            {
                // เรียกตรงไปที่ AudioSource เลยจะไวที่สุด (ถ้าทำได้)
                SoundManager.Instance.PlaySFX(SoundManager.Instance.birdSfx);
                lastBirdTime = Time.time;
            }
        }
    }
}

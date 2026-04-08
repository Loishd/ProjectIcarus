using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatShield : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    void Update()
    {
        if (player.transform.position.y > transform.position.y + 10)
        {
            Destroy(gameObject);
        }
    }
    public void SetData(PlayerMovement playerRef)
    {
        player = playerRef;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("HeatShield");
            PlayerStatus.Instance.isHeatShield = true;
            Destroy(gameObject);
        }
    }
}

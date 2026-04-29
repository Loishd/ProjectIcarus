using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatShield : MonoBehaviour
{
    [SerializeField] PlayerMovement player;

    private void Start()
    {
        player = PlayerStatus.Instance._playerReference;
    }
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
            if (PlayerStatus.Instance.isHeatShield)
            {
                player.ExtendItemHeatShield();
            }
            else
            {
                PlayerStatus.Instance.isHeatShield = true;
            }
            Debug.Log("HeatShield");
            Destroy(gameObject);
        }
    }
}

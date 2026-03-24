using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attraction : MonoBehaviour
{
    [SerializeField] PlayerMovement player;

    private void Awake()
    {

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
            Debug.Log("Magnet");
            PlayerStatus.Instance.isMagnetic = true;
            Destroy(gameObject);
        }
    }
}

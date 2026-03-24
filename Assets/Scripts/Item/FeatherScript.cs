using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherScript : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] CoinSpawning coinSpawning;
    [SerializeField] FeverSystem feverSystem;
    [SerializeField] private float feverGain = 5f;

    private Vector2 playerTarget;
    // Start is called before the first frame update
    void Start()
    {
        //if (ScoreManager.Instance != null)
        //{
        //    Debug.Log("Mae mung tai");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > transform.position.y +10)
        {
            coinSpawning._coinAmount--;
            Destroy(gameObject);
        }
    }

    public void SetData(PlayerMovement playerRef, CoinSpawning coinSpawnerRef, FeverSystem feverSystemRef)
    {
        player = playerRef;
        coinSpawning = coinSpawnerRef;
        feverSystem = feverSystemRef;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore(1);
            feverSystem.IncreaseFever(feverGain);
            coinSpawning._coinAmount--;
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Magnet"))
        {
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherScript : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] CoinSpawning coinSpawning;
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

    public void SetData(PlayerMovement playerRef, CoinSpawning coinSpawnerRef)
    {
        player = playerRef;
        coinSpawning = coinSpawnerRef;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Luy");
            ScoreManager.Instance.AddScore(1);
            coinSpawning._coinAmount--;
            Destroy(gameObject);
        }
    }
}

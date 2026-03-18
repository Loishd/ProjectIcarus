using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] CoinSpawning coinSpawning;
    [SerializeField] PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > transform.position.y + 10)
        {
            coinSpawning._coinAmount--;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Cloud do what
        }
    }

    public void SetData(PlayerMovement playerRef, CoinSpawning coinSpawningRef)
    {
        player = playerRef;
        coinSpawning = coinSpawningRef;
    }
}

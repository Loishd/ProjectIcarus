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
        player = GetComponent<PlayerMovement>();
        coinSpawning = GetComponent<CoinSpawning>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayer(PlayerMovement playerRef)
    {
        player = playerRef;
    }
    public void SetCoinSpawner(CoinSpawning coinSpawnerRef)
    {
        coinSpawning = coinSpawnerRef;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null)
        {
            //ADd player score
            coinSpawning._coinAmount--;
            Destroy(gameObject);
        }
    }
}

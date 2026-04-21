using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] CoinSpawning coinSpawning;
    [SerializeField] PlayerMovement player;
    [SerializeField] FeverSystem feverSystem;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || coinSpawning == null) return;

        if (player.transform.position.y > transform.position.y)
        {
            Destroy(gameObject,10);
        }


    }

    public void SetData(PlayerMovement playerRef, CoinSpawning coinSpawningRef, FeverSystem feverSystemRef)
    {
        player = playerRef;
        coinSpawning = coinSpawningRef;
        feverSystem = feverSystemRef;   
    }
}

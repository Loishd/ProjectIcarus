using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] CoinSpawning coinSpawning;
    [SerializeField] PlayerMovement player;
    [SerializeField] FeverSystem feverSystem;

    void Update()
    {
        if (player == null) return;

        if (player.transform.position.y > transform.position.y + 5f)
        {
            Destroy(gameObject);
        }
    }

    public void SetData(PlayerMovement playerRef, CoinSpawning coinSpawningRef, FeverSystem feverSystemRef)
    {
        player = playerRef;
        coinSpawning = coinSpawningRef;
        feverSystem = feverSystemRef;
    }
}
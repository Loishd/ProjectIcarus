using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle1 : MonoBehaviour
{
    [SerializeField] CoinSpawning coinSpawning;
    [SerializeField] PlayerMovement player;
    [SerializeField] List<Bird> bird = new List<Bird>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < bird.Count; i++)
        {
            bird[i].SetData(player, coinSpawning);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetObstacleData(PlayerMovement playerRef, CoinSpawning coinSpawningRef)
    {
        player = playerRef;
        coinSpawning = coinSpawningRef;
    }
}

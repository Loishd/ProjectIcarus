using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] List<Transform> spawnedPositions = new List<Transform>();
    [SerializeField] List<GameObject> ObjectLists = new List<GameObject>();
    [SerializeField] float obstacleAmount;
    [SerializeField] float obstacleMax;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (obstacleAmount < obstacleMax)
        {
            System.Random rnd = new System.Random();
            int objectRandomizer = rnd.Next(0, spawnedPositions.Count);
            int positionRandom = rnd.Next(0, spawnedPositions.Count);
            GameObject spawnedObject = Instantiate(ObjectLists[objectRandomizer], spawnedPositions[positionRandom]);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] CoinSpawning coinSpawner;
    [SerializeField] Transform player;
    [SerializeField] Transform _mapParent;
    [SerializeField] Transform firstSpawnPos;
    [SerializeField] SpriteRenderer kanChak;

    int MapCount;
    int mapNum;
    int mapNumNew;
    SpriteRenderer Map;

    [SerializeField] private float _currentPattern;
    public float CurrentPattern => _currentPattern;

    [SerializeField] List<GameObject> mapList = new List<GameObject>();

    float HighestOfSkip;

    bool hasSpawnedSkip = false;
    bool isWaitingForKanchak = false; // 🔥 LOCK SYSTEM

    void Start()
    {
        mapNum = Random.Range(0, mapList.Count);

        Map = Instantiate(mapList[mapNum],firstSpawnPos.position,Quaternion.identity,_mapParent).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CheckBackgroundChange();
    }

    // 🔥 CALLED WHEN PATTERN SPAWNS
    public void AddPattern()
    {
        if (isWaitingForKanchak) return; // ❌ STOP COUNTING DURING TRANSITION

        _currentPattern++;

        if (_currentPattern == 10 && !hasSpawnedSkip)
        {
            SpawnSkipScene();
            isWaitingForKanchak = true; // 🔒 LOCK
        }
    }

    public void CheckBackgroundChange()
    {
        // wait until player passes kanchak
        if (isWaitingForKanchak && player.position.y > HighestOfSkip)
        {
            SpawnMap();
            // 🔥 RESET EVERYTHING CLEAN
            _currentPattern = 0;
            isWaitingForKanchak = false;
            hasSpawnedSkip = false;
        }
    }

    void SpawnSkipScene()
    {
        hasSpawnedSkip = true;

        SpriteRenderer changeScene = Instantiate(
            kanChak,
            new Vector3(0f, coinSpawner.HighestInPattern + 20f, 0f),
            Quaternion.identity
        );

        HighestOfSkip = changeScene.transform.position.y + 20;
    }

    public void SpawnMap()
    {
        if (MapCount == 0)
        {
            mapNumNew = Random.Range(0, mapList.Count);
            if (mapNumNew != mapNum)
            {
                Debug.Log("Instantiated");
                GameObject newMap = Instantiate(mapList[mapNum], new Vector3(0, player.position.y + 400, 0), Quaternion.identity, _mapParent).GetComponent<SpriteRenderer>().gameObject;
                Map = newMap.GetComponentInChildren<SpriteRenderer>();
                MapCount++;
            }
        }
    }
    public int DestroyMap()
    {
        Destroy( Map.gameObject , 2);
        return MapCount = 0;
    }
}
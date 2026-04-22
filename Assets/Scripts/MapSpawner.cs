using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] int PatternLimit;
    [SerializeField] CoinSpawning coinSpawner;
    [SerializeField] Transform player;
    [SerializeField] Transform _mapParent;
    [SerializeField] SpriteRenderer actualMap;
    [SerializeField] SpriteRenderer kanChak;
    int mapNum;
    int mapNumNew;

    [SerializeField] private float _currentPattern;
    public float CurrentPattern => _currentPattern;

    [SerializeField] List<Sprite> mapList = new List<Sprite>();

    float HighestOfSkip;
    bool hasSpawnedSkip = false;
    bool isWaitingForKanchak = false;
    [SerializeField] bool isSpawnEmpty;
    public Action changedScene;

    public bool IsSpawnEmpty => isSpawnEmpty;

    void Start()
    {
        Time.timeScale = 1.0f;
        mapNum = UnityEngine.Random.Range(0, mapList.Count);
        actualMap = transform.Find("map").GetComponent<SpriteRenderer>();
        actualMap.sprite = mapList[mapNum];
    }

    void Update()
    {
        CheckBackgroundChange();
    }
    public void AddPattern()
    {
        if (isWaitingForKanchak) return; 

        _currentPattern++;

        if (_currentPattern == PatternLimit && !hasSpawnedSkip)
        {
            SpawnSkipScene();
            isWaitingForKanchak = true;
        }
    }

    public void CheckBackgroundChange()
    {
        if (isWaitingForKanchak && player.position.y > HighestOfSkip)
        {
            SpawnSkipScene();
            _currentPattern = 0;
            isWaitingForKanchak = false;
            hasSpawnedSkip = false;
            isSpawnEmpty = true;
        }
    }

    void SpawnSkipScene()
    {
        hasSpawnedSkip = true;
        SpriteRenderer changeScene = Instantiate(kanChak,new Vector3(0f, coinSpawner.HighestInPattern + 50f, 0f),Quaternion.identity);
        HighestOfSkip = changeScene.transform.position.y + 20;
    }

    public void ChangeMapPosition()
    {
        mapNumNew = UnityEngine.Random.Range(0, mapList.Count);
        while (mapNum == mapNumNew)
        {
            mapNumNew = UnityEngine.Random.Range(0, mapList.Count);
        }
        StartCoroutine(TimerChangeMap(2));
        isSpawnEmpty = true;
    }

    public void DestroySkipScene()
    {

    }

    public IEnumerator TimerChangeMap(int secondWaited)
    {
        yield return new WaitForSeconds(secondWaited);
        actualMap.transform.position = new Vector3(actualMap.transform.position.x, player.transform.position.y + 250f, actualMap.transform.position.z);
        actualMap.sprite = mapList[mapNumNew];
        Debug.Log("Changed Background");
    }

    public void IsNotSpawnEmpty()
    {
        isSpawnEmpty = false;
    }
}
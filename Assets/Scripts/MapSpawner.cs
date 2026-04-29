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
    int mapNum;
    int mapNumNew;

    [SerializeField] private float _currentPattern;
    public float CurrentPattern => _currentPattern;

    [SerializeField] List<Sprite> mapList = new List<Sprite>();
    [SerializeField] bool isSpawnEmpty;
    bool isChangingScene;

    public bool IsSpawnEmpty => isSpawnEmpty;
    [SerializeField] float startPosY;

    private float timer = 0;
    void Start()
    {
        startPosY = gameObject.transform.position.y;
        Time.timeScale = 1.0f;
        mapNum = UnityEngine.Random.Range(0, mapList.Count);
        actualMap = transform.Find("map").GetComponent<SpriteRenderer>();
        actualMap.sprite = mapList[mapNum];
    }

    void Update()
    {
        if (ScoreManager.Instance.isPause) return;
        if (isChangingScene)
        {
            TimerChangeMap(2);
        }
    }
    public void AddPattern()
    {
        _currentPattern++;
    }

    public void ChangeMapPosition()
    {
        Debug.Log("Called");
        mapNumNew = UnityEngine.Random.Range(0, mapList.Count);
        while (mapNum == mapNumNew)
        {
            mapNumNew = UnityEngine.Random.Range(0, mapList.Count);
        }
        isChangingScene = true;
    }

    public void TimerChangeMap(int secondWaited)
    {
        timer += Time.deltaTime;
        if (timer >= secondWaited)
        {
            Debug.Log("Map Reposition");
            gameObject.transform.position = new Vector3(actualMap.transform.position.x, startPosY, actualMap.transform.position.z);
            actualMap.sprite = mapList[mapNumNew];
            isChangingScene = false;
        }
    }

    public void ResetPattern()
    {
        coinSpawner.ResetAfterSkip();
        coinSpawner.SetPatternAmount(0);
        _currentPattern = 0;
    }
}
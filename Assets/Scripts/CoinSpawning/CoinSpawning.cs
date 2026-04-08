using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CoinSpawning : MonoBehaviour
{
    [SerializeField] FeverSystem feverSystem;
    [SerializeField] PlayerMovement player;
    [SerializeField] MapSpawner mapSpawner;
    [Header("Coin Pattern")]
    [SerializeField] List<Pattern1> PatternList = new List<Pattern1>();
    [SerializeField] Transform spawnedFeatherParent;
    [SerializeField] float CoinGap;
    [SerializeField] public int _coinAmount;
    [SerializeField] int PatternMax;
    [SerializeField] int currentPattern;
    public int CurrentPattern => currentPattern;

    Vector3 _highestInpattern;

    public float HighestInPattern => _highestInpattern.y;

    void Start()
    {
        _highestInpattern = player.transform.position;

        // spawn initial patterns
        for (int i = 0; i < PatternMax; i++)
        {
            AutoSpawn();
        }
    }

    void Update()
    {
        if ((currentPattern < PatternMax) && mapSpawner.CurrentPattern != 10)
        {
            AutoSpawn();
        }
    }

    public void AutoSpawn()
    {
        if (mapSpawner.IsSpawnEmpty)
        {
            SpawnEmpty();
            mapSpawner.IsNotSpawnEmpty();
        }
        else
        {
            int patternNum = Random.Range(0, PatternList.Count);
            GameObject spawnedPattern = Instantiate(PatternList[patternNum].gameObject, new Vector3(0, _highestInpattern.y, 0), Quaternion.identity, spawnedFeatherParent);
            Pattern1 pattern = spawnedPattern.GetComponent<Pattern1>();
            pattern.SetPatternData(player, this, feverSystem);
            _highestInpattern = pattern.GetHighestPos() + new Vector3(0, -5, 0);
            _highestInpattern.y += CoinGap; // ⭐ prevent overlap
            mapSpawner.AddPattern();
            currentPattern++;
            _coinAmount += pattern.FeatherList.Count;
        }
    }

    public void SpawnEmpty()
    {
        int patternNum = Random.Range(0, PatternList.Count);
        GameObject spawnedPattern = Instantiate(PatternList[patternNum].gameObject, new Vector3(0, _highestInpattern.y + 50, 0), Quaternion.identity, spawnedFeatherParent);
        Pattern1 pattern = spawnedPattern.GetComponent<Pattern1>();
        pattern.SetPatternData(player, this, feverSystem);
        _highestInpattern = pattern.GetHighestPos() + new Vector3(0, -5, 0);
        _highestInpattern.y += CoinGap; // ⭐ prevent overlap
        mapSpawner.AddPattern();
        currentPattern++;
        _coinAmount += pattern.FeatherList.Count;
    }

    public void DecreasePattern()
    {
        currentPattern--;
    }
}
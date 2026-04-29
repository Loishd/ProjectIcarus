using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CoinSpawning : MonoBehaviour
{
    [SerializeField] GameObject SceneSkipper;
    [SerializeField] FeverSystem feverSystem;
    [SerializeField] PlayerMovement player;
    [SerializeField] MapSpawner mapSpawner;
    [Header("Coin Pattern")]
    [SerializeField] List<Pattern1> PatternList = new List<Pattern1>();
    [SerializeField] Transform spawnedFeatherParent;
    [SerializeField] int PatternMax;
    [SerializeField] int currentPattern;
    [SerializeField] float patternGap;
    public int CurrentPattern => currentPattern;
    [Header("Scene Skipper")]
    [SerializeField] bool hasSpawnedSceneSkipper = false;
    [SerializeField] float sceneSkipperOffsetY = 2f;
    [SerializeField] Vector3 skipScenePosition;
    Vector3 _highestInpattern;
    public float HighestInPattern => _highestInpattern.y;

    public static CoinSpawning Instance { get; private set; }

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Maintain only one instance
            return;
        }
    }

    void Start()
    {
        _highestInpattern = player.transform.position;
    }

    void Update()
    {
        if (ScoreManager.Instance.isPause) return;
        if ((currentPattern < PatternMax) && mapSpawner.CurrentPattern != PatternMax)
        {
            AutoSpawn();
        }
    }

    public void AutoSpawn()
    {
        int patternNum = Random.Range(0, PatternList.Count);
        GameObject spawnedPattern = Instantiate(PatternList[patternNum].gameObject, new Vector3(0, _highestInpattern.y, 0), Quaternion.identity, spawnedFeatherParent);
        Pattern1 pattern = spawnedPattern.GetComponent<Pattern1>();
        pattern.SetPatternData(player, this, feverSystem);
        pattern.SpawnItem();
        _highestInpattern = pattern.GetHighestPos() + new Vector3(0, patternGap, 0);
        mapSpawner.AddPattern();
        currentPattern++;
        if (currentPattern == PatternMax)
        {
            GameObject sceneSkipper = Instantiate(SceneSkipper, new Vector3(0, _highestInpattern.y + 30, 0), Quaternion.identity, spawnedFeatherParent);
        }
    }

    public void DecreasePattern()
    {
        currentPattern--;
    }

    public void SetPatternAmount(int amount)
    {
        currentPattern = amount;
    }

    public void ResetAfterSkip()
    {
        _highestInpattern = new Vector3 (player.transform.position.x, player.transform.position.y + 10, player.transform.position.z);
    }
}
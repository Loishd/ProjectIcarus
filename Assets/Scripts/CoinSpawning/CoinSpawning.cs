using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawning : MonoBehaviour
{
    [SerializeField] FeverSystem feverSystem;
    [SerializeField] PlayerMovement player;

    [Header("Coin Pattern")]
    [SerializeField] List<Pattern1> PatternList = new List<Pattern1>();

    [SerializeField] Transform spawnedFeatherParent;
    [SerializeField] float CoinGap;
    [SerializeField] public int _coinAmount;
    [SerializeField] int PatternMax;
    [SerializeField] int currentPattern;

    [Header("ChangePattern")]
    [SerializeField] float PAtternCount;
    public int CurrentPattern => currentPattern;

    Vector3 HighestInpattern;

    void Start()
    {
        HighestInpattern = player.transform.position;

        // spawn initial patterns
        for (int i = 0; i < PatternMax; i++)
        {
            AutoSpawn();
        }
    }

    void Update()
    {
        if (currentPattern < PatternMax)
        {
            AutoSpawn();
        }
    }

    public void AutoSpawn()
    {
        int patternNum = Random.Range(0, PatternList.Count);

        GameObject spawnedPattern = Instantiate(
            PatternList[patternNum].gameObject,
            new Vector3(0, HighestInpattern.y, 0),
            Quaternion.identity,
            spawnedFeatherParent
        );

        Pattern1 pattern = spawnedPattern.GetComponent<Pattern1>();

        pattern.SetPatternData(player, this, feverSystem);

        HighestInpattern = pattern.GetHighestPos() + new Vector3(0, -5, 0);
        HighestInpattern.y += CoinGap; // ⭐ prevent overlap

        currentPattern++;
        _coinAmount += pattern.FeatherList.Count;
    }

    public void DecreasePattern()
    {
        currentPattern--;
    }
}
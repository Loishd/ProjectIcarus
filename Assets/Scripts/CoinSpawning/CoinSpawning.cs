using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawning : MonoBehaviour
{
    [SerializeField] FeverSystem feverSystem;
    [SerializeField] PlayerMovement player;

    [Header("Coin Pattern")]
    [SerializeField] List <Pattern1> PatternList = new List<Pattern1>();

    [Header("Obstacle Pattern")]
    [SerializeField] Obstacle1 obstacle1;

    [SerializeField] Transform spawnedFeatherParent;
    [SerializeField] float CoinGap;
    [SerializeField] public int _coinAmount;
    [SerializeField] int _coinMax;
    [SerializeField] int PatternMax;
    [SerializeField] int currentPattern;
    public int CurrentPattern => currentPattern;

    Vector3 HighestInpattern;
    float LatestY;
    // Start is called before the first frame update
    void Start()
    {
        HighestInpattern = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        AutoSpawn();
    }

    public void AutoSpawn()
    {

        if (currentPattern < PatternMax)
        {
            int PatternNum = Random.Range(0, PatternList.Count);
            GameObject spawnedPattern1 = Instantiate(PatternList[PatternNum].gameObject, new Vector3(0, HighestInpattern.y, 0), Quaternion.identity, spawnedFeatherParent);
            Pattern1 _pattern1 = spawnedPattern1.GetComponent<Pattern1>();
            _pattern1.SetPatternData(player, this,feverSystem);
            _pattern1.GetHighestPos();
            HighestInpattern = _pattern1.GetHighestPos();
            currentPattern++;
            _coinAmount += _pattern1.FeatherList.Count;
        }
    }

    public void DecreasePattern()
    {
        currentPattern--;
    }
}

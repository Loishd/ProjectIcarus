using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawning : MonoBehaviour
{
    
    [SerializeField] PlayerMovement player;

    [Header("Coin Pattern")]
    [SerializeField] Pattern1 pattern1;

    [Header("Obstacle Pattern")]
    [SerializeField] Obstacle1 obstacle1;

    [SerializeField] Transform spawnedFeatherParent;
    [SerializeField] List<Transform> lanes = new List<Transform>();
    [SerializeField] GameObject feather;
    [SerializeField] float CoinGap;
    [SerializeField] public int _coinAmount;
    [SerializeField] int _coinMax;
    float LatestY;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //SpawnPattern1();
        //SpawnCoin(_coinMax);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnPattern1();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SpawnObstacle1();
        }
    }
    public void SpawnCoin(int MaxCoin)
    {
        if (_coinAmount < MaxCoin)
        {
            int lanesIndex = UnityEngine.Random.Range(0, lanes.Count);
            Vector3 nextSpawnPos = new Vector3(lanes[lanesIndex].position.x, LatestY + CoinGap, 0);
            if (_coinAmount == 0)
            {
                GameObject firstFeather = Instantiate(feather, new Vector3(lanes[lanesIndex].position.x, 10, 0), Quaternion.identity, spawnedFeatherParent);
                FeatherScript featherScript = firstFeather.GetComponent<FeatherScript>();
                featherScript.SetData(player, this);
                LatestY = firstFeather.transform.position.y;
                _coinAmount++;
            }
            else
            {
                GameObject firstFeather = Instantiate(feather, nextSpawnPos, Quaternion.identity, spawnedFeatherParent);
                FeatherScript featherScript = firstFeather.GetComponent<FeatherScript>();
                featherScript.SetData(player, this);
                LatestY = firstFeather.transform.position.y;
                _coinAmount++;
            }
        }
    }
    public void SpawnPattern1()
    {
        GameObject spawnedPattern1 = Instantiate(pattern1.gameObject, new Vector3(0, player.transform.position.y, 0), Quaternion.identity, spawnedFeatherParent);
        Pattern1 _pattern1 = spawnedPattern1.GetComponent<Pattern1>();
        _pattern1.SetPatternData(player, this);
        _coinAmount += _pattern1.FeatherList.Count;
    }

    public void SpawnObstacle1()
    {
        GameObject spawnedObstacle1 = Instantiate(obstacle1.gameObject, new Vector3(0, player.transform.position.y, 0), Quaternion.identity, spawnedFeatherParent);
        Obstacle1 _obstacle1 = spawnedObstacle1.GetComponent<Obstacle1>();
        _obstacle1.SetObstacleData(player, this);
        //_coinAmount += _obstacle1.FeatherList.Count;
    }
}

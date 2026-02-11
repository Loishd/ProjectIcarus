using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawning : MonoBehaviour
{
    [SerializeField] Transform spawnedFeatherParent;
    [SerializeField] List<Transform> lanes = new List<Transform>();
    [SerializeField] GameObject feather;
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
        SpawnCoin(_coinMax);
    }
    public void SpawnCoin(int MaxCoin)
    {
        if (_coinAmount < MaxCoin)
        {
            int lanesIndex = UnityEngine.Random.Range(0, lanes.Count);
            Vector3 nextSpawnPos = new Vector3(lanes[lanesIndex].position.x, LatestY + 3, 0);
            if (_coinAmount == 0)
            {
                GameObject firstFeather = Instantiate(feather, lanes[lanesIndex].position, Quaternion.identity, spawnedFeatherParent);
                LatestY = firstFeather.transform.position.y;
                _coinAmount++;
            }
            else
            {
                GameObject firstFeather = Instantiate(feather, nextSpawnPos, Quaternion.identity, spawnedFeatherParent);
                LatestY = firstFeather.transform.position.y;
                _coinAmount++;
            }
        }
    }
}

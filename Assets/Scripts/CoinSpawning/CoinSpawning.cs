using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawning : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
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
        SpawnCoin(_coinMax);
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
}

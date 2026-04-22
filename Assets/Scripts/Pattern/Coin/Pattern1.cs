using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern1 : MonoBehaviour
{
    protected FeverSystem feverSystem;
    protected CoinSpawning coinSpawning;
    protected PlayerMovement player;

    [SerializeField] List<Transform> itemSpawnPos = new List<Transform>();
    [SerializeField] List<GameObject> itemList = new List<GameObject>();
    [SerializeField] List<FeatherScript> featherList = new List<FeatherScript>();
    [SerializeField] List<Bird> birdList = new List<Bird>();
    [SerializeField] Transform HighestOne;

    Vector3 highestPos;

    public List<FeatherScript> FeatherList => featherList;

    protected virtual void Start()
    {
        if (HighestOne != null)
            highestPos = HighestOne.position;
    }


    public virtual void SetPatternData(PlayerMovement player, CoinSpawning coinSpawning, FeverSystem feverSystem)
    {
        this.player = player;
        this.coinSpawning = coinSpawning;
        this.feverSystem = feverSystem;

        // ⭐ IMPORTANT: assign to all children
        FeatherScript[] feathers = GetComponentsInChildren<FeatherScript>();
        Bird[] birds = GetComponentsInChildren<Bird>();

        foreach (var f in feathers)
        {
            f.SetData(player, coinSpawning, feverSystem);
        }

        foreach (var b in birds)
        {
            b.SetData(player, coinSpawning, feverSystem);
        }
    }

    public virtual Vector3 GetHighestPos()
    {
        return HighestOne.position;
    }

    protected virtual void Update()
    {
        if (player == null) return;

        float highestY = GetHighestPos().y;

        if (player.transform.position.y > highestY + 2f)
        {
            coinSpawning.DecreasePattern();
            Destroy(gameObject, 10);
        }
    }

    public virtual void SpawnItem()
    {
        int result = Random.Range(0, 3);
        int itemPos = Random.Range(0, itemSpawnPos.Count);
        int itemNum = Random.Range(0, itemList.Count);
        if ((result == 0))
        {
            GameObject spawnedItem = Instantiate(itemList[itemNum], itemSpawnPos[itemPos].transform.position, Quaternion.identity, this.transform);
        }
        else 
        {
            return;
        }
    }
}
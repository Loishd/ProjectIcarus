using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern1 : MonoBehaviour
{
    [SerializeField] CoinSpawning coinSpawning;
    [SerializeField] PlayerMovement player;
    [SerializeField] List<FeatherScript> featherList = new List<FeatherScript>();

    public List<FeatherScript> FeatherList => featherList;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < featherList.Count; i++)
        {
            featherList[i].SetData(player, coinSpawning);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPatternData(PlayerMovement playerRef, CoinSpawning coinSpawningRef)
    {
        player = playerRef;
        coinSpawning = coinSpawningRef;
    }
}

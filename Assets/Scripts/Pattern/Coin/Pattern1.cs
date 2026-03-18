using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern1 : MonoBehaviour
{
    [SerializeField] CoinSpawning coinSpawning;
    [SerializeField] PlayerMovement player;
    [SerializeField] List<FeatherScript> featherList = new List<FeatherScript>();
    [SerializeField] Vector3 highestPos;
    public Vector3 HighestPos => highestPos;

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
        if (featherList[featherList.Count - 1] == null)
        {
            Destroy(gameObject);
            coinSpawning.DecreasePattern();
        }

        for (int i = 0; i < featherList.Count; i++)
        {
            if (featherList[i] == null)
            {
                featherList.RemoveAt(i);
            }
        }
    }

    public void SetPatternData(PlayerMovement playerRef, CoinSpawning coinSpawningRef)
    {
        player = playerRef;
        coinSpawning = coinSpawningRef;
    }

    public Vector3 GetHighestPos()
    {
        return featherList[featherList.Count - 1].transform.position;
    }
}

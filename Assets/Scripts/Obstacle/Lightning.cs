using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] CoinSpawning coinSpawning;
    [SerializeField] PlayerMovement player;
    [SerializeField] float WarningTime;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < WarningTime; i++)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.Death();
        }
    }

    public void SetData(PlayerMovement playerRef, CoinSpawning coinSpawningRef)
    {
        player = playerRef;
        coinSpawning = coinSpawningRef;
    }

    public IEnumerator SpawnLightning(float WaitingTime)
    {
        //play warning animation?
        yield return new WaitForSeconds(WaitingTime);
        //spawn cloud at the top of the screen
        gameObject.SetActive(true); //flashes lane red.
        //spawn lightning
        //wait for lightning
        //fade away
        //despawn cloud
        yield return null;
    }
}

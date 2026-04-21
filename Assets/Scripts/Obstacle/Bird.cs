using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] CoinSpawning coinSpawning;
    [SerializeField] PlayerMovement player;
    [SerializeField] FeverSystem feverSystem;
    [SerializeField] private int nearMissIncreaseScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || coinSpawning == null) return;

        if (player.transform.position.y > transform.position.y)
        {
            Destroy(gameObject, 10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

            if (PlayerStatus.Instance.isFever)
                return;

            if (PlayerStatus.Instance.isInvulnerability)
            {
                PlayerStatus.Instance.isInvulnerability = false;
                return;
            }
            else
            {
                player.Death();
            }
        }

        if (collision.gameObject.CompareTag("Wing"))
        {
            ScoreManager.Instance._currentScore += nearMissIncreaseScore;
            PlayerStatus.Instance.nearMissCount += 1;

            if ((PlayerPrefs.GetInt("IcarusArrogance") != 1) && PlayerStatus.Instance.nearMissCount >= 15)
            {
                PlayerPrefs.SetInt("IcarusArrogance", 1);
                StartCoroutine(RewardManager.Instance.PopUpQuest("Icarus Arrogance"));
            }
        }

    }

    public void SetData(PlayerMovement playerRef,CoinSpawning coinSpawningRef,FeverSystem feverSystemRef)
    {
        player = playerRef;
        coinSpawning = coinSpawningRef;
        feverSystem = feverSystemRef;
    }
}

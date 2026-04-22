using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmWind1 : MonoBehaviour
{
    [SerializeField] CoinSpawning coinSpawning;
    [SerializeField] PlayerMovement player;
    [SerializeField] HeightSystem heightSys;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float increaseAmount;
    [SerializeField] private float nearMissIncreaseScore;


    void Start()
    {
        heightSys = GameObject.FindWithTag("GameController").GetComponent<HeightSystem>();
    }

    void Update()
    {
        if (player.transform.position.y > transform.position.y)
        {
            Destroy(gameObject, 20);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            heightSys.IncreaseHeight(increaseAmount);
            PlayerStatus.Instance.touchWindCount += 1;

            if ((PlayerPrefs.GetInt("AggressiveTyphoon") != 1) && PlayerStatus.Instance.touchWindCount >= 15)
            {
                PlayerPrefs.SetInt("AggressiveTyphoon", 1);
                StartCoroutine(RewardManager.Instance.PopUpQuest("Aggressive Typhoon"));
            }
        }

        if (collision.gameObject.CompareTag("Wing"))
        {
            StartCoroutine(PlayerStatus.Instance.TriggerNearMiss(nearMissIncreaseScore));
        }
    }
    void Move()
    {
        Vector3 movement = new Vector3(0f, -1f, 0f).normalized;

        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    public void SetData(PlayerMovement playerRef, CoinSpawning coinSpawningRef, FeverSystem feverSystemRef)
    {
        player = playerRef;
        coinSpawning = coinSpawningRef;
    }
}

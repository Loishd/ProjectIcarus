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



    void Start()
    {
        heightSys = GameObject.FindWithTag("GameController").GetComponent<HeightSystem>();
    }

    void Update()
    {
        Move();
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
                Debug.Log("Aggressive Typhoon Completed!");
            }
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

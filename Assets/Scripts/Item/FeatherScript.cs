using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherScript : MonoBehaviour
{
    [SerializeField] public PlayerMovement player;
    [SerializeField] public CoinSpawning coinSpawning;
    [SerializeField] public FeverSystem feverSystem;
    [SerializeField] private float feverGain = 5f;
    [SerializeField] bool isTargettingPlayer;
    [SerializeField] float targetSpeed;
    Rigidbody2D rigidbody2d;
    private Vector2 playerTarget;
    Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        //if (ScoreManager.Instance != null)
        //{
        //    Debug.Log("Mae mung tai");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        MagnetMethod();
        playerTarget = player.transform.position;

        if (player.transform.position.y > transform.position.y + 10)
        {
            coinSpawning._coinAmount--;
            Destroy(gameObject);
        }
    }

    public void SetData(PlayerMovement playerRef, CoinSpawning coinSpawnerRef, FeverSystem feverSystemRef)
    {
        player = playerRef;
        coinSpawning = coinSpawnerRef;
        feverSystem = feverSystemRef;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore(1);
            feverSystem.IncreaseFever(feverGain);
            coinSpawning._coinAmount--;
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Magnet"))
        {
           isTargettingPlayer = true;
        }
    }

    public void MagnetMethod()
    {
        if (isTargettingPlayer)
        {
            transform.position = Vector3.Lerp(transform.position, playerTarget, targetSpeed * Time.deltaTime);
        }
    }
}

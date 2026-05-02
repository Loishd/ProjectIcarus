using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] CoinSpawning coinSpawning;
    [SerializeField] PlayerMovement player;
    [SerializeField] FeverSystem feverSystem;
    [SerializeField] private int nearMissIncreaseScore;

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || coinSpawning == null) return;

        if (player.transform.position.y > transform.position.y)
        {
            Destroy(gameObject, 20);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spriteRenderer.enabled = false;

            if (PlayerStatus.Instance.isInvulnerability)
            {
                PlayerStatus.Instance.isInvulnerability = false;
                player.ResetInvulnerabilityTime();
                return;
            }
            else
            {
                if (player.godMode) return;
                player.SetDeathAnimation();
            }

            if (PlayerStatus.Instance.isFever)
                return;   
        }

        if (collision.gameObject.CompareTag("Wing"))
        {
            StartCoroutine(PlayerStatus.Instance.TriggerNearMiss(nearMissIncreaseScore));
        }

    }

    public void SetData(PlayerMovement playerRef,CoinSpawning coinSpawningRef,FeverSystem feverSystemRef)
    {
        player = playerRef;
        coinSpawning = coinSpawningRef;
        feverSystem = feverSystemRef;
    }
}

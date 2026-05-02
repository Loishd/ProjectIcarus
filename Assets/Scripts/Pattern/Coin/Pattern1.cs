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


    private void FixedUpdate()
    {
        if (PlayerStatus.Instance.isDeath) return;
        if (ScoreManager.Instance.isPause) return;
        AutoMove();
        if (HighestOne == null) return;
    }

    public void AutoMove()
    {
        Vector3 movement = new Vector3(0f, -1f, 0f).normalized;

        transform.Translate(movement * PlayerStatus.Instance.MoveSpeedRef * Time.deltaTime * PlayerStatus.Instance.speedIncrease);
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
        if (HighestOne != null)
            return HighestOne.position;

        return transform.position + Vector3.up * 5f;
    }

    protected virtual void Update()
    {
        if (PlayerStatus.Instance.isDeath) return;
        if (player == null) return;
        if (HighestOne == null) return;

        float highestY = GetHighestPos().y;

        if (player.transform.position.y > highestY + 2f)
        {
            Destroy(gameObject, 10);
            //coinSpawning.DecreasePattern();
        }
    }

    public virtual void SpawnItem()
    {
        int result = Random.Range(0, 3);
        int itemPos = Random.Range(0, itemSpawnPos.Count);
        int itemNum = Random.Range(0, itemList.Count);

        if (result != 0) return;

        GameObject spawnedItem = Instantiate(itemList[itemNum],itemSpawnPos[itemPos].position,Quaternion.identity);

        // 🔥 บังคับ parent ชัวร์ ๆ
        spawnedItem.transform.SetParent(this.transform, true);

        // 🧪 debug เช็ค
        Debug.Log("Spawned: " + spawnedItem.name);
        Debug.Log("Parent: " + spawnedItem.transform.parent);
    }
}
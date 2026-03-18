using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedObject : MonoBehaviour
{
    private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y > 100)
        {
            moveSpeed = -10;
        }
        else if (gameObject.transform.position.y < -100)
        {
            moveSpeed = 10;
        }
        //AutoWalk();
    }

    void AutoWalk()
    {
        Vector3 movement = new Vector3(0f, 1f, 0f).normalized;

        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}

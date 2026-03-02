using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpawnedCoin : MonoBehaviour
{
    public float moveSpeed = 5f;

    private void FixedUpdate()
    {
        AutoWalk();
    }

    void AutoWalk()
    {
        Vector3 movement = new Vector3(0f, 1f, 0f).normalized;

        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    public float moveSpeed = 5f;
    public int currentLane = 1;
    public float laneDistance = 5f;
    public float changeSpeed = 5f;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) Death();
        LaneSwapper();
    }

    void FixedUpdate()
    {
        AutoWalk();
    }

    void AutoWalk()
    {
        Vector3 movement = new Vector3(0f, 1f, 0f).normalized;

        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    void LaneSwapper()
    {
        //Check Lane
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentLane == 0) return;
            currentLane--;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentLane == 2) return;
            currentLane++;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (currentLane == 0) targetPosition += Vector3.left * laneDistance;
        else if (currentLane == 2) targetPosition += Vector3.right * laneDistance;

        transform.position = Vector3.Lerp(transform.position, targetPosition, changeSpeed * Time.deltaTime);
    }

    public void Death()
    {
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}

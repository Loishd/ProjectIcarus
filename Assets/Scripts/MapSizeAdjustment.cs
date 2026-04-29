using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSizeAdjustment : MonoBehaviour
{
    [SerializeField] HeightSystem heightSystem;
    [SerializeField] private float smoothSpeed;

    void Update()
    {
        float currentZ = transform.position.z;
        float targetZ = heightSystem.CurrentHeight;

        float newZ = Mathf.Lerp(currentZ, targetZ, Time.deltaTime * smoothSpeed);

        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }

    private void FixedUpdate()
    {
        if (ScoreManager.Instance.isPause) return;
        AutoWalk();
    }

    void AutoWalk()
    {
        Vector3 movement = new Vector3(0f, -1f, 0f).normalized;

        transform.Translate(movement * PlayerStatus.Instance.MoveSpeedRef * Time.deltaTime * PlayerStatus.Instance.speedIncrease);
    }
}

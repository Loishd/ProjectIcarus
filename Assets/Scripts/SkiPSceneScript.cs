using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiPSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        AutoWalk();
    }

    void AutoWalk()
    {
        Vector3 movement = new Vector3(0f, -1f, 0f).normalized;

        transform.Translate(movement * PlayerStatus.Instance.MoveSpeedRef * Time.deltaTime * PlayerStatus.Instance.speedIncrease);
    }
}

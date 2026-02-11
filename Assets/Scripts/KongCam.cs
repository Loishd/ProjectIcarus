using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KongCam : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    float CamPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CamPos = player.transform.position.y;
        gameObject.transform.position = new Vector3(0, (float)(CamPos + 2.5), -10);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSizeAdjustment : MonoBehaviour
{
    [SerializeField] HeightSystem heightSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, heightSystem.CurrentHeight); 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDisplayer : MonoBehaviour
{
    [SerializeField] PlayerMovement _player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.Instance.isPause) return;
        gameObject.transform.position = new Vector3(
            _player.transform.position.x,
            _player.transform.position.y,
            _player.transform.position.z - 1
        );
    }
}

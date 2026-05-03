using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ImageLooper : MonoBehaviour
{
    [SerializeField] float timer;
    [SerializeField] int currentSprite;
    [SerializeField] float changeTime;
    Image image;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();

    private void Start()
    {
        timer = changeTime;
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            
            else
            {
                currentSprite++;

                if (currentSprite > sprites.Count - 1)
                currentSprite = 0;

                image.sprite = sprites[currentSprite];
                timer = changeTime;
            }
                

        }
    }
}

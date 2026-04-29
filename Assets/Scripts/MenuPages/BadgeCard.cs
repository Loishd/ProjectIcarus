using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadgeCard : MonoBehaviour
{
    [SerializeField] Sprite newSprite;
    [SerializeField] string getPlayerPrefs;
    private Image badgeImage;
    void Start()
    {
        badgeImage = GetComponent<Image>();

        if (PlayerPrefs.GetInt(getPlayerPrefs) >= 1)
            badgeImage.sprite = newSprite;  
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] List<Sprite> imageList = new List<Sprite>();
    private Image image;
    [SerializeField] int currrentPage;

    private void Start()
    {
        image = GetComponent<Image>();
        currrentPage = 0;

        if (!PlayerPrefs.HasKey("HasLaunchedBefore"))
            gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currrentPage++;
            if (currrentPage > imageList.Count - 1) 
                currrentPage = 0;

            image.sprite = imageList[currrentPage];
        }
    }
}

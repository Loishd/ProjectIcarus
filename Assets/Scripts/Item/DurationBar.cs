using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurationBar : MonoBehaviour
{
    public Slider slider;
    public float currentDuration;
    public Image itemImage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SetDuration(10);
        }

        RunDuration();
    }

    public void SetDuration(float duration)
    {
        currentDuration = duration;
        slider.value = duration;
    }

    void RunDuration()
    {
        slider.maxValue = currentDuration;

        if (currentDuration > 0)
        {
            currentDuration -= 1 * Time.deltaTime;
            slider.value = currentDuration;
        }
    }
}

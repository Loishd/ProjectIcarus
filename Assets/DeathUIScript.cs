using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathUIScript : MonoBehaviour
{
    [SerializeField] TMP_Text Score;
    [SerializeField] TMP_Text Feather;

    public void UpdateDeathUIText()
    {
        Score.text = ((int)ScoreManager.Instance._currentScore).ToString();
        Feather.text = ScoreManager.Instance.CurrentCoins.ToString();
    }
}

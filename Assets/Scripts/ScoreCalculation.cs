using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class ScoreCalculation : MonoBehaviour
{
    [SerializeField] float _currentScore;
    [SerializeField] float distance;
    [SerializeField] HeightSystem heightSystem;
    [SerializeField] TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        _currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _currentScore += (float)(Time.deltaTime * (1 * math.abs(heightSystem.CurrentHeight - 50/ 10)) * math.pow(10, 1));
        scoreText.text = _currentScore.ToString();
    }
}

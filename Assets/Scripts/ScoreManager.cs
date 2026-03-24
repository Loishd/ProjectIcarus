using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] TMP_Text CoinText;
    [SerializeField] TMP_Text HighestScoreText;
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] TMP_Text MultiplierText;

    public float InvulnerabilityMultiplier;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] CoinSpawning coinSpawning;
    [SerializeField] int _currentCoins;
    [SerializeField] float _highestScore;
    public float _currentScore;
    public float multiplier = 1f;
    [SerializeField] HeightSystem heightSystem;
    [SerializeField] float increaseAmount;

    [Header("Sound")]
    [SerializeField] AudioSource audioSource;


    public int CurrentCoins => _currentCoins;
    // Start is called before the first frame update
    public static ScoreManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Destroy(gameObject); // sometimes causes issues
        }
    }
    void Start()
    {
        InvulnerabilityMultiplier = 1;
        _currentCoins = 0;
        _highestScore = PlayerPrefs.GetFloat("HighestScore");
        HighestScoreText.text = ((int)_highestScore).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _currentScore += (float)(Time.deltaTime * (1 + math.abs((heightSystem.CurrentHeight-50)/ 10)) * increaseAmount * InvulnerabilityMultiplier);
        //MultiplierText.text = "x" + Mathf.Round(1+math.abs((heightSystem.CurrentHeight-50) / 10)).ToString();
        multiplier = 1 + Mathf.Abs((heightSystem.CurrentHeight - 50f) / 10f);
        MultiplierText.text = "x" + multiplier.ToString("F2");
        ScoreText.text = ((int)_currentScore).ToString();
        CoinText.text = "Coins: " + _currentCoins.ToString();
        //currentScore = playerStats.currentscore 

        UpdateHighestScore();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenAndCloseMenu();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            _currentCoins += 1000;
        }

    }

    public void AddScore(int coins)
    {
        _currentCoins += coins;
    }

    public void OpenAndCloseMenu()
    {
        if (!PauseMenu.activeSelf)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void UpdateHighestScore()
    {
        if (_currentScore > _highestScore)
        {
            _highestScore = _currentScore;
            HighestScoreText.text = ((int)_highestScore).ToString();
        }
    }
}

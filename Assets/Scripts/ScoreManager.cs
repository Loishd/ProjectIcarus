using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] TMP_Text CoinText;
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] TMP_Text MultiplierText;

    [SerializeField] GameObject PauseMenu;
    [SerializeField] CoinSpawning coinSpawning;
    [SerializeField] int _currentCoins;
    [SerializeField] float _currentScore;
    [SerializeField] HeightSystem heightSystem;
    [SerializeField] float increaseAmount;

    public int CurrentCoins => _currentCoins;
    // Start is called before the first frame update
    public static ScoreManager Instance;
    private void Awake()
    {
        if (Instance != null) Destroy(this.gameObject); else DontDestroyOnLoad(this.gameObject); Instance = this;
    }
    void Start()
    {
        _currentCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _currentScore += (float)(Time.deltaTime * (1 + math.abs((heightSystem.CurrentHeight-50)/ 10)) * increaseAmount);
        MultiplierText.text = "x" + (1+math.abs((heightSystem.CurrentHeight-50) / 10)).ToString();
        ScoreText.text = _currentScore.ToString();
        CoinText.text = "Coins: " + _currentCoins.ToString();
        //currentScore = playerStats.currentscore 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenAndCloseMenu();
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
}

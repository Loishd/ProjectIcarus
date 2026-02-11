using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] CoinSpawning coinSpawning;
    [SerializeField] int _currentScore;
    [SerializeField] TMP_Text ScoreText;

    public int CurrentScore => _currentScore;
    // Start is called before the first frame update
    public static ScoreManager Instance;
    private void Awake()
    {
        if (Instance != null) Destroy(this.gameObject); else DontDestroyOnLoad(this.gameObject); Instance = this;
    }
    void Start()
    {
        _currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = _currentScore.ToString();
        //currentScore = playerStats.currentscore 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenAndCloseMenu();
        }
        
    }

    public void AddScore(int score)
    {
        _currentScore+=score;
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

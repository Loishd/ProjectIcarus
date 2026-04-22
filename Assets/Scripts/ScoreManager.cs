using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] TMP_Text CoinText;
    [SerializeField] TMP_Text HighestScoreText;
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] TMP_Text MultiplierText;

    public float InvulnerabilityMultiplier;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] List<GameObject> PauseCountDown = new List<GameObject>();
    [SerializeField] CoinSpawning coinSpawning;
    [SerializeField] int _currentCoins;
    [SerializeField] float _highestScore;
    public float _currentScore;
    public float multiplier = 1f;
    [SerializeField] HeightSystem heightSystem;
    [SerializeField] float increaseAmount;

    [Header("Sound")]
    [SerializeField] AudioSource audioSource;

    [Header("Quests")]
    [SerializeField] float volatileFlightReachTime = 360f;
    [SerializeField] float seekingForPoseidonReachTime = 480f;

    [Header("NameReceive")]
    public GameObject nameInputPanel;
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] private string playerName;
    [SerializeField] TMP_Text enterNameWarning;

    [Header("UI")]
    [SerializeField] GameObject Wings;

    [Header("Gadget")]
    [SerializeField] float gadgetMultiplier1;
    [SerializeField] float gadgetMultiplier2;
    [SerializeField] float gadgetMultiplier3;

    int i = 0;
    float timer = 0f;
    public int CurrentCoins => _currentCoins;
    bool isCountingDown;
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
        ShowReceiverPanel();
        InvulnerabilityMultiplier = 1;
        _currentCoins = 0;
        _highestScore = PlayerPrefs.GetFloat("HighestScore");
        HighestScoreText.text = ((int)_highestScore).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCountingDown) return;
        if (PlayerStatus.Instance.isDeath)
        {
            Wings.SetActive(false);
        }
        GadgetMultiplierUpdate();
        _currentScore += (float)(Time.deltaTime * multiplier) * increaseAmount * InvulnerabilityMultiplier;
        //MultiplierText.text = "x" + Mathf.Round(1+math.abs((heightSystem.CurrentHeight-50) / 10)).ToString();
        MultiplierText.text = "x" + multiplier.ToString("F2");
        ScoreText.text = playerName + ": " + ((int)_currentScore).ToString();
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

        CheckHeightQuest();
    }

    public void AddScore(int coins)
    {
        _currentCoins += coins;
    }

    public void OpenAndCloseMenu()
    {
        if (PlayerStatus.Instance.isDeath) return;
        if (!PauseMenu.activeSelf)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Continue();
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

    void CheckHeightQuest()
    {
        float questMultiplier = 1 + (heightSystem.CurrentHeight - 50f) / 10f;

        if (questMultiplier >= 4)
        {
            //Volatile Flight
            float timer = 0f;
            timer += Time.deltaTime;

            if (questMultiplier < 4)
                timer = 0f;

            if (timer >= volatileFlightReachTime && (PlayerPrefs.GetInt("VolatileFlight") != 1))
            {
                PlayerPrefs.SetInt("VolatileFlight", 1);
                Debug.Log("Volatile Flight Completed!");
            }
 
        }

        else if (questMultiplier <= -4)
        {
            float timer = 0f;
            timer += Time.deltaTime;

            if (questMultiplier > -4)
                timer = 0f;

            if (timer >= seekingForPoseidonReachTime && (PlayerPrefs.GetInt("SeekingForPoseidon") != 1))
            {
                PlayerPrefs.SetInt("SeekingForPoseidon", 1);
                Debug.Log("Seeking for Poseidon Completed!");
            }

        }
    }
    public void ShowReceiverPanel()
    {
        Time.timeScale = 0f;
        nameInputPanel.gameObject.SetActive(true);
    }

    public void ReceivedName()
    {
        if (nameInput.text == "")
        {
            enterNameWarning.gameObject.SetActive(true);
            return;
        }
        Time.timeScale = 1.0f;
        nameInputPanel.gameObject.SetActive(false);
        playerName = nameInput.text;
        Debug.Log(playerName);
        SoundManager.Instance.PlayCurrentMusic();
        Wings.SetActive(true);
    }

    public void GadgetMultiplierUpdate()
    {
        if (PlayerStatus.Instance.gadgetIndex == 1)
        {
            multiplier = 1 + Mathf.Abs((heightSystem.CurrentHeight - 50f) / 10f) * gadgetMultiplier1;
        }
        else if (PlayerStatus.Instance.gadgetIndex == 2)
        {
            multiplier = 1 + Mathf.Abs((heightSystem.CurrentHeight - 50f) / 10f) * gadgetMultiplier2;
        }
        else if (PlayerStatus.Instance.gadgetIndex == 3)
        {
            multiplier = 1 + Mathf.Abs((heightSystem.CurrentHeight - 50f) / 10f) * gadgetMultiplier3;
        }
        else
        {
            multiplier = 1 + Mathf.Abs((heightSystem.CurrentHeight - 50f) / 10f);
        }
    }
    public void Continue()
    {
        PauseMenu.SetActive(false);
        isCountingDown = true;
        StartCoroutine(CountingDown());
    }

    public IEnumerator CountingDown()
    {
        PauseCountDown[0].SetActive(true);
        yield return new WaitForSeconds(1);
        PauseCountDown[0].SetActive(false);
        PauseCountDown[1].SetActive(true);
        yield return new WaitForSeconds(1);
        PauseCountDown[1].SetActive(false);
        PauseCountDown[2].SetActive(true);
        yield return new WaitForSeconds(1);
        PauseCountDown[2].SetActive(false);
        Time.timeScale = 1;
        isCountingDown = false;
    }
}

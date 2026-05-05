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
    float volatileTimer = 0f;
    [SerializeField] float seekingForPoseidonReachTime = 480f;
    float poseidonTimer = 0f;

    [Header("NameReceive")]
    public GameObject nameInputPanel;
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] private string playerName;
    [SerializeField] TMP_Text enterNameWarning;

    [Header("MVP")]
    [SerializeField] string _mvpName = "SomChai";
    [SerializeField] float _playerHighestScore;

    [Header("UI")]
    [SerializeField] GameObject Wings;

    [Header("Gadget")]
    [SerializeField] float gadgetMultiplier1;
    [SerializeField] float gadgetMultiplier2;
    [SerializeField] float gadgetMultiplier3;

    [Header("CountDown")]
    [SerializeField] TMP_Text countDownText;
    [SerializeField] GameObject countDownPanel;

    int i = 3;
    float timer = 0f;
    public bool isPause;
    [SerializeField] FeverSystem _feverSystem;
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
        if (isCountingDown)
        {
            CountDown();
            if (Input.anyKey) return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (nameInputPanel.activeSelf)
            {
                return;
            }
            OpenAndCloseMenu();
        }

        if (isPause) return;
        if (isCountingDown) return;

        if (PlayerStatus.Instance.isDeath)
        {
            Wings.SetActive(false);
            return;
        }

        GadgetMultiplierUpdate();
        UpdateHighestScore();
        CheckHeightQuest();

        _currentScore += (float)(Time.deltaTime * multiplier) * increaseAmount * InvulnerabilityMultiplier;
        //MultiplierText.text = "x" + Mathf.Round(1+math.abs((heightSystem.CurrentHeight-50) / 10)).ToString();
        MultiplierText.text = "x" + multiplier.ToString("F2");
        ScoreText.text = playerName + ": " + ((int)_currentScore).ToString();
        CoinText.text = _currentCoins.ToString();
        //currentScore = playerStats.currentscore 
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
            isPause = true;
            PauseMenu.SetActive(true);
        }
        else
        {
            isCountingDown = true;
            PauseMenu.SetActive(false);
        }
    }

    void UpdateHighestScore()
    {
        if (_currentScore > _highestScore)
        {
            _highestScore = _currentScore;
            HighestScoreText.text = playerName + ": " + ((int)_highestScore).ToString();

            PlayerPrefs.SetString("MVPName", playerName);
        }
        else
        {
            _mvpName = PlayerPrefs.GetString("MVPName");
            HighestScoreText.text = _mvpName + ": " + ((int)_highestScore).ToString();
        }
    }

    void CheckHeightQuest()
    {
        float questMultiplier = 1 + (heightSystem.CurrentHeight - 50f) / 10f;

        if (questMultiplier >= 4)
        {
            //Volatile Flight

            volatileTimer += Time.deltaTime;

            if (questMultiplier < 4)
                volatileTimer = 0f;

            if (volatileTimer >= volatileFlightReachTime && (PlayerPrefs.GetInt("VolatileFlight") != 1))
            {
                PlayerPrefs.SetInt("VolatileFlight", 1);
                StartCoroutine(RewardManager.Instance.PopUpQuest("Volatile Flight"));
            }
 
        }

        else if (questMultiplier <= -4)
        {
            poseidonTimer += Time.deltaTime;

            if (questMultiplier > -4)
                poseidonTimer = 0f;

            if (poseidonTimer >= seekingForPoseidonReachTime && (PlayerPrefs.GetInt("SeekingForPoseidon") != 1))
            {
                PlayerPrefs.SetInt("SeekingForPoseidon", 1);
                StartCoroutine(RewardManager.Instance.PopUpQuest("Seeking for Poseidon"));

                if (PlayerPrefs.GetInt("CanEquipDiveModule") != 1)
                    PlayerPrefs.SetInt("CanEquipDiveModule", 1);
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
            multiplier = 1 + Mathf.Abs((heightSystem.CurrentHeight - 50f) / 10f) * gadgetMultiplier1 * _feverSystem.UseFeverMultiplier;
        }
        else if (PlayerStatus.Instance.gadgetIndex == 2)
        {
            multiplier = 1 + Mathf.Abs((heightSystem.CurrentHeight - 50f) / 10f) * gadgetMultiplier2 * _feverSystem.UseFeverMultiplier;
        }
        else if (PlayerStatus.Instance.gadgetIndex == 3)
        {
            multiplier = 1 + Mathf.Abs((heightSystem.CurrentHeight - 50f) / 10f) * gadgetMultiplier3 * _feverSystem.UseFeverMultiplier;
        }
        else
        {
            multiplier = 1 + Mathf.Abs((heightSystem.CurrentHeight - 50f) / 10f) * _feverSystem.UseFeverMultiplier;
        }
    }

    public void CountDown()
    {
        countDownPanel.SetActive(true);
        countDownText.text = i.ToString();

        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            timer = 0f;

            if (i <= 1)
            {
                countDownPanel.SetActive(false);
                isCountingDown = false;
                isPause = false;
                i = 3;
            }
            else
            {
                i--;
            }
        }
    }


}

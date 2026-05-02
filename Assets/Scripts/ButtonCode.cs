using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonCode : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        Time.timeScale = 1.0f;
        ScoreManager.Instance.OpenAndCloseMenu();
    }

    public void Retry(string name)
    {
        Time.timeScale = 1.0f;
        deathScreen.SetActive(false);
        LoadingScene(name);
    }

    public void LoadingScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1.0f;
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Play(string sceneName)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName);
    }

    public void Options(GameObject soundMenu)
    {
        soundMenu.SetActive(true);
    }

    public void OffOptions(GameObject soundMenu)
    {
        soundMenu.SetActive(false);
    }
}

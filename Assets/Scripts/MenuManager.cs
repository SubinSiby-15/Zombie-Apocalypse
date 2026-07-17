using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject pausePanel;
   // public GameObject optionsPanel;

    private bool isPaused = false;

    //-----------------------------------
    // START GAME
    //-----------------------------------
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    //-----------------------------------
    // SETTINGS
    //-----------------------------------
    public void OpenSettings()
    {
        Debug.Log("Settings Opened");
    }

    //-----------------------------------
    // OPTIONS
    //-----------------------------------
    public void OpenOptions()
    {
        Debug.Log("Options Opened");
    }

    //-----------------------------------
    // BACK
    //-----------------------------------
    public void Back()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    //-----------------------------------
    // PAUSE
    //-----------------------------------
    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    //-----------------------------------
    // RESUME
    //-----------------------------------
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    //-----------------------------------
    // OPTIONS
    //-----------------------------------
    public void Openoptions()
    {
        Debug.Log("Options Opened");
    }

    //-----------------------------------
    // MAIN MENU
    //-----------------------------------
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    //-----------------------------------
    // QUIT
    //-----------------------------------
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    //-----------------------------------
    // ESC PAUSE
    //-----------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }
}
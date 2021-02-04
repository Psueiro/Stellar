using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour, IUpdate
{
    public Console console;
    public GameObject mainMenu;
    public GameObject pauseGraphic;
    public GameObject winPopUp;
    public GameObject lossPopUp;
    public StatPasser statPasser;
    public KeyCode menuKey;
    public KeyCode consoleKey;
    public float levelRestartDelay;
    public MonetizationManager adManager;

    void Awake()
    {
        UpdateManager.SubscribeToUpdateList(this);
        EventManager.SubscribeToEvent("Win", ActivateWinPopUp);
        EventManager.SubscribeToEvent("Loss", DelayedLevelRestart);
        EventManager.SubscribeToEvent("GameOver", DelayedGameOverCall);
    }

    public void OnUpdate()
    {
        if (Input.GetKeyDown(menuKey))
        {
            Menu();
        }

        if (Input.GetKeyDown(consoleKey))
        {
            OpenConsole();
        }
    }

    public void OpenConsole()
    {
        if (mainMenu.activeSelf) return;
        Pause();
        console.gameObject.SetActive(!console.gameObject.activeSelf);
    }

    public void Pause()
    {
        pauseGraphic.SetActive(!pauseGraphic.activeSelf);
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }

    public void RestartLevel()
    {
        StatPasser newStatPasser = Instantiate(statPasser);
        newStatPasser.restart = true;
        UpdateManager.ClearUpdateList();
        UnsubscribeToAll();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        if (console.gameObject.activeSelf) return;
        Pause();
        mainMenu.SetActive(!mainMenu.activeSelf);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        UpdateManager.ClearUpdateList();
        UnsubscribeToAll();
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToLevel(string s)
    {
        UnsubscribeToAll();
        Time.timeScale = 1;
        SceneManager.LoadScene(s);
    }

    void ActivateWinPopUp()
    {
        Time.timeScale = 0;
        //adManager.ShowAds();
        UpdateManager.ClearUpdateList();
        winPopUp.SetActive(true);
    }

    void UnsubscribeToAll()
    {
        EventManager.UnsubscribeToEvent("Win", ActivateWinPopUp);
        EventManager.UnsubscribeToEvent("Loss", DelayedLevelRestart);
        EventManager.UnsubscribeToEvent("GameOver", DelayedGameOverCall);
    }

    void DelayedGameOverCall()
    {
        StartCoroutine(GameOverPopUp());
    }

    IEnumerator GameOverPopUp()
    {
        yield return new WaitForSeconds(levelRestartDelay);
        StopAllCoroutines();
        ActivateLossPopUp();
    }

    void ActivateLossPopUp()
    {
        Time.timeScale = 0;
        UpdateManager.ClearUpdateList();
        adManager.ShowAds();
        UnsubscribeToAll();
        lossPopUp.SetActive(true);
    }

    void DelayedLevelRestart()
    {
        StartCoroutine(LevelRestart());
    }

    IEnumerator LevelRestart()
    {
        yield return new WaitForSeconds(levelRestartDelay);
        StopAllCoroutines();
        RestartLevel();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controlsPanel;
    public GameObject tutorialPanel;

    // Pauses the game.
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    // Unpauses the game.
    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }

    // Starts the game.
    public void PlayGame()
    {
        UnpauseGame();
        SceneManager.LoadScene("Game");
    }

    // Ends the game.
    public void ExitGame()
    {
        Application.Quit();
    }

    // Unpauses and restarts the game.
    public void RestartGame()
    {
        UnpauseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Unpauses and opens the main menu scene.
    public void GoToMainMenu()
    {
        UnpauseGame();
        SceneManager.LoadScene("Main Menu");
    }

    // Pauses and opens the tutorial info box.
    public void OpenTutorial()
    {
        PauseGame();
        tutorialPanel.SetActive(true);
    }

    // Unpauses and closes the tutorial info box.
    public void CloseTutorial()
    {
        UnpauseGame();
        tutorialPanel.SetActive(false);
    }

    // Pauses and opens the controls info box.
    public void OpenControls()
    {
        PauseGame();
        controlsPanel.SetActive(true);
    }

    // Unpauses and closes the controls info box.
    public void CloseControls()
    {
        UnpauseGame();
        controlsPanel.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class PauseMenu : MonoBehaviour
{

    public static bool isPaused = false;
    [SerializeField] public GameObject pauseMenuUI;
    [SerializeField] public GameObject instructionsPanel;
    [SerializeField] public GameObject optionsPanel;
    [SerializeField] private TextMeshProUGUI instructionsButtonText;
    [SerializeField] private Button instructionsButton;
    [SerializeField] private Button optionsAcceptButton;
    [SerializeField] public Button resumeButton;
    
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused && !instructionsPanel.activeSelf && !optionsPanel.activeSelf) {
                Resume();
            } else {
                Pause();
            }
        }
        
    }

    public void Pause() {
        if (instructionsPanel.activeSelf || optionsPanel.activeSelf) {
            return;
        }
        pauseMenuUI.SetActive(true);
        resumeButton.Select();
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Instructions() {
        instructionsPanel.SetActive(true);
        instructionsButtonText.text = "got it!";
        instructionsButton.Select();
    }

    public void Options() {
        pauseMenuUI.SetActive(false);
        optionsPanel.SetActive(true);
        optionsAcceptButton.Select();
    }

    public void LoadMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{

    public static bool isPaused = false;
    [SerializeField] public GameObject pauseMenuUI;
    [SerializeField] public GameObject instructionsPanel;
    [SerializeField] public Button resumeButton;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isPaused) {
                Resume();
            } else {
                Pause();
            }
        }
        
    }

    public void Pause() 
    {
        if (instructionsPanel.activeSelf) {
            return;
        }
        pauseMenuUI.SetActive(true);
        resumeButton.Select();
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume() 
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void LoadMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() {
        Application.Quit();
    }
}

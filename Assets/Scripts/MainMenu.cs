using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] public Button playButton;
    [SerializeField] public GameObject optionsPanel;
    [SerializeField] public GameObject creditsPanel;
    [SerializeField] public GameObject titleAndButtons;

    void Start() {
        playButton.Select();
    }

    public void PlayGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void Options() {
        optionsPanel.SetActive(true);
    }

    public void Credits() {
        titleAndButtons.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void ExitCredits() {
        creditsPanel.SetActive(false);
        titleAndButtons.SetActive(true);
    }

    public void Quit() {
        Application.Quit();
    }
}

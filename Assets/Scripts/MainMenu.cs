using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] public Button playButton;
    [SerializeField] public GameObject optionsPanel;
    [SerializeField] public Button optionsAcceptButton;
    [SerializeField] public GameObject creditsPanel;
    [SerializeField] public Button creditsButton;
    [SerializeField] public Button creditBackButton;
    [SerializeField] public GameObject titleAndButtons;

    void Start() {
        playButton.Select();
    }

    public void PlayGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void Options() {
        optionsPanel.SetActive(true);
        optionsAcceptButton.Select();
    }

    public void Credits() {
        titleAndButtons.SetActive(false);
        creditsPanel.SetActive(true);
        creditBackButton.Select();
    }

    public void ExitCredits() {
        creditsPanel.SetActive(false);
        titleAndButtons.SetActive(true);
        creditsButton.Select();
    }

    public void Quit() {
        Application.Quit();
    }
}

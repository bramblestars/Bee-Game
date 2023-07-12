using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] public Button playButton;

    void Start() {
        playButton.Select();
    }

    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

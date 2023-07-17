using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionPanel : MonoBehaviour
{
    [SerializeField] GameObject instructions;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Button pauseMenuInstructionsButton;
    [SerializeField] GameObject optionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return) && !pauseMenu.activeSelf) {
            DismissInstructions();
        }
    }

    // Update is called once per frame
    public void DismissInstructions()
    {
        if (!pauseMenu.activeSelf && !optionsMenu.activeSelf) {
            Time.timeScale = 1f;
        }
        instructions.SetActive(false);
        pauseButton.SetActive(true);
        if (pauseMenu.activeSelf) {
            pauseMenuInstructionsButton.Select();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionPanel : MonoBehaviour
{
    [SerializeField] GameObject instructions;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            DismissInstructions();
        }
    }

    // Update is called once per frame
    public void DismissInstructions()
    {
        if (!pauseMenu.activeSelf) {
            Time.timeScale = 1f;
        }
        instructions.SetActive(false);
        pauseButton.SetActive(true);
    }
}

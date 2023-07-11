using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionPanel : MonoBehaviour
{
    [SerializeField] GameObject instructions;
    [SerializeField] GameObject pauseButton; 

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
    }

    // Update is called once per frame
    public void DismissInstructions()
    {
        Time.timeScale = 1f;
        instructions.SetActive(false);
        pauseButton.SetActive(true);
    }
}

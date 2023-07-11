using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionPanel : MonoBehaviour
{
    [SerializeField] public GameObject instructions;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    public void DismissInstructions()
    {
        Time.timeScale = 1f;
        instructions.SetActive(false);
    }
}

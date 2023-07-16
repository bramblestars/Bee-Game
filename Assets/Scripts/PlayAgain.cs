using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    [SerializeField] public Bee bee;
    [SerializeField] GameObject panel;

    public void Replay() {
        bee.transform.rotation = new Quaternion(0, 0, 0, 0);
        bee.transform.position = new Vector3(0, 8.35f, 0);
        bee.pollen = 0;
        bee.quotaMet = 40;
        panel.SetActive(false);
    }
}

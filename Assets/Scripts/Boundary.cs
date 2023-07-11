using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boundary : MonoBehaviour
{

    [SerializeField] private Image cantGoFartherMessage;
    private bool timerRunning;
    private double timer;

    void Start() {
        cantGoFartherMessage.CrossFadeAlpha(0, 0f, false);
    }

    void Update() {
        if (timerRunning) {
            timer += Time.deltaTime;
            if (timer >= 2.0) {
                cantGoFartherMessage.CrossFadeAlpha(0, 1f, false);
                timerRunning = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        cantGoFartherMessage.CrossFadeAlpha(1, 1f, false);
    }

    void OnTriggerExit2D(Collider2D other) {
        timerRunning = true; 
    }


}

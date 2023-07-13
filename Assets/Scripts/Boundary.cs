using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boundary : MonoBehaviour
{

    [SerializeField] private Image cantGoFartherMessage;
    [SerializeField] private float horizontalOffset = 140f;
    [SerializeField] private float verticalOffset = 90f;

    private bool timerRunning;
    private double timer;
    RectTransform rectTransform;

    void Start() {
        cantGoFartherMessage.CrossFadeAlpha(0, 0f, false);
        rectTransform = cantGoFartherMessage.GetComponent<RectTransform>();
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
        
        switch(gameObject.tag) {
            case "BoundaryRight":
                rectTransform.localPosition = new Vector3(horizontalOffset, 0, 0);
                break;
            case "BoundaryLeft":
                rectTransform.localPosition = new Vector3(-horizontalOffset, 0, 0);
                break;
            case "BoundaryTop":
                rectTransform.localPosition = new Vector3(0, verticalOffset, 0);
                break;
            case "BoundaryBottom":
                rectTransform.localPosition = new Vector3(0, -verticalOffset, 0);
                break;
        }
    }

    void OnTriggerExit2D(Collider2D other) {

        timerRunning = true; 
    }


}

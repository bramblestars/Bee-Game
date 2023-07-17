using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bee : MonoBehaviour
{
    [SerializeField] float speed = 900f;
    [SerializeField] float rotationSpeed = 0.4f;
    [SerializeField] int pollenLossRate = 1;
    [SerializeField] int pollenPerFlower = 40;
    [SerializeField] int pollenMax = 200;
    [SerializeField] int quota = 600;
    [SerializeField] public int quotaMet = 40;

    [SerializeField] GameObject bee;
    [SerializeField] private Image pollenBar;
    [SerializeField] private Image quotaCircle;
    [SerializeField] private GameObject youWinPanel;
    [SerializeField] private Button youWinReplayButton;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button gameOverReplayButton;
    [SerializeField] private TextMeshProUGUI gameTimer;

    public double timer = 0.0;
    public double pollenLossTimer = 0.0;
    public int pollen = 0;
    
    private Rigidbody2D rb2D;
    private bool rotatingCounterClockwise;
    private int numTimesOutOfBounds = 0;
    private float timeInsideBoundary;
    private float lerpSpeed;
    private Vector2 pushVector;

    // Start is called before the first frame update
    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

        if (!youWinPanel.activeSelf && !gameOverPanel.activeSelf) {

            // Move and rotate the bee
            float currentRotation = bee.transform.eulerAngles.z;
            if (currentRotation > 180) {
                currentRotation = currentRotation - 360f;
            } 

            RotateAndTransform(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), currentRotation);

            // Update total timer
            timer += Time.deltaTime;

            // Update the pollen amount every 2 seconds
            pollenLossTimer += Time.deltaTime;
            if (pollenLossTimer >= 2.0) {
                pollen -= pollenLossRate;
                quotaMet -= pollenLossRate / 2;
                if (pollen < 0) {
                    pollen = 0;
                }
                if (quotaMet < 0) {
                    gameOverPanel.SetActive(true);
                    gameOverReplayButton.Select();
                }
                pollenLossTimer = 0.0;
            }

            Color color = quotaCircle.color;

            // change the color to red if quotaMet is less than a fifth of max quota
            color.g = Mathf.Lerp(color.g, quotaMet < quota / 5 ? 80f / 255f : 228f / 255f, Time.deltaTime);
            quotaCircle.color = color;
        } 
        
        else {
            rb2D.velocity = Vector2.zero;
        }

        lerpSpeed = 3f * Time.deltaTime;

        PollenSmoothFill();
        
    }

    // Fill the pollen bars smoothly using lerp
    private void PollenSmoothFill() {
        quotaCircle.fillAmount = Mathf.Lerp(quotaCircle.fillAmount, (float)quotaMet / (float)quota, lerpSpeed);
        pollenBar.fillAmount = Mathf.Lerp(pollenBar.fillAmount, (float)pollen / (float)pollenMax, lerpSpeed);
    }

    /// <summary>
    /// Rotates the bee and moves it forward depending on the SerializeFields speed and rotationSpeed 
    /// </summary>
    /// <param name="vertical"> vertical user input
    /// <param name="horizontal"> horizontal user input
    /// <param name="currentRotation"> the bee's current rotation transform
    private void RotateAndTransform(float vertical, float horizontal, float currentRotation) {
        
        // Do not move or rotate if no current user input
        if (horizontal == 0f && vertical == 0f) {
            rb2D.MoveRotation(currentRotation);
            rb2D.velocity = Vector2.zero;
            return;
        }

        float rotationDirection;
        float currentRad = currentRotation * Mathf.Deg2Rad;

        // vector representing user input
        Vector2 moveVector = new Vector2(horizontal, vertical);
        // direction the bee is currently moving towards
        Vector2 currentVector = new Vector2(-Mathf.Sin(currentRad), Mathf.Cos(currentRad));
        // pushing force when bee hits world boundary
        Vector2 pushForce = Vector2.zero;

        // push bee out of boundaries if bee is inside. pushVector is set inside OnTriggerEnter2D for boundary trigger colliders
        if (numTimesOutOfBounds > 0) {
            timeInsideBoundary += Time.deltaTime;
            pushForce = pushVector * speed / 2f * (1f + timeInsideBoundary);
        }

        float rotationAngle = Vector2.Angle(Vector2.up, moveVector);

        // rotate the bee in the right direction when approaching the vertical vector (rotation angle 0)
        if (MathF.Abs(currentRotation) < 10f) {
            rotationDirection = horizontal > 0 ? -1f : 1f;
        }

        if (horizontal > 0) {
            rotationAngle = -rotationAngle;
        }

        if (Vector2.Angle(currentVector, moveVector) > 150f) {
            rb2D.MoveRotation(currentRotation);
            rb2D.velocity = -speed * currentVector.normalized + pushForce;
            return;
        }

        if (MathF.Abs(rotationAngle - currentRotation) < 15f) {
            rotationDirection = 0f;
        }
        
        if (rotationAngle > 0) {
            rotationDirection = (rotationAngle - 180f <= currentRotation && currentRotation <= rotationAngle) ? 1f : -1f;
        }

        else {
            rotationDirection = (rotationAngle <= currentRotation && currentRotation <= rotationAngle + 180f)? -1f : 1f;
        }

        // set rotation (if above a certain threshold, move towards user input angle by set speed, otherwise, just change rotation to user input angle)
        if (Vector2.Angle(currentVector, moveVector) > 4.0f) {
            rb2D.MoveRotation(currentRotation + rotationSpeed * rotationDirection);
        } 
        
        else {
            rb2D.MoveRotation(rotationAngle);
        }
        
        // set speed
        rb2D.velocity = speed * currentVector.normalized + pushForce;

    }

    void OnTriggerEnter2D(Collider2D other) {
        switch (other.tag) {
            // we use numTimesOutOfBounds to keep track of if the bee is in the boundary in case the bee is hitting
            // two boundaries at once (i.e. in the corner)
            case "BoundaryTop": 
                pushVector = pushVector + Vector2.down;
                numTimesOutOfBounds += 1;
                break;
            case "BoundaryBottom": 
                pushVector = pushVector + Vector2.up;
                numTimesOutOfBounds +=1 ;
                break;
            case "BoundaryLeft":
                pushVector = pushVector + Vector2.right;
                numTimesOutOfBounds += 1;
                break;
            case "BoundaryRight":
                pushVector = pushVector + Vector2.left;
                numTimesOutOfBounds += 1;
                break;
            case "Hive":
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                quotaMet += pollen;

                // show the "You Win" panel and set the timer
                if (quotaMet >= quota) {
                    youWinPanel.SetActive(true);
                    youWinReplayButton.Select();

                    int minutes = (int) Math.Floor(timer / 60);
                    int seconds = (int) Math.Floor(timer % 60);
                    string mins = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();
                    string secs = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();

                    gameTimer.text = "<color=#ffec9b>time:</color> " + mins + ":" + secs;
                }

                pollen = 0;
                break;
            case "Flower":
                pollen += pollenPerFlower;
                if (pollen > pollenMax) {
                    pollen = pollenMax;
                } 
                break;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "BoundaryTop": 
                pushVector = pushVector - Vector2.down;
                numTimesOutOfBounds -= 1;
                break;
            case "BoundaryBottom": 
                pushVector = pushVector - Vector2.up;
                numTimesOutOfBounds -= 1;
                break;
            case "BoundaryLeft":
                pushVector = pushVector - Vector2.right;
                numTimesOutOfBounds -= 1;
                break;
            case "BoundaryRight":
                pushVector = pushVector - Vector2.left;
                numTimesOutOfBounds -= 1;
                break;
        }

        if (numTimesOutOfBounds == 0)
        {
            timeInsideBoundary = 0f;
        }

    }

}

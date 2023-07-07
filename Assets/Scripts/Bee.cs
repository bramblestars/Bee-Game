using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bee : MonoBehaviour
{
    [SerializeField] float speed = 900f;
    [SerializeField] float rotationSpeed = 0.4f;
    [SerializeField] int pollenLossRate = 1;
    [SerializeField] int pollenRate = 40;
    [SerializeField] int pollenMax = 200;

    [SerializeField] GameObject bee;
    [SerializeField] private Image pollenBar;

    public int health = 3; 
    public double timer = 0.0;
    
    private Rigidbody2D rb2D;
    private bool rotatingCounterClockwise;
    private int numTimesOutOfBounds = 0;
    private int pollen = 0;
    private float timeInsideBoundary;
    private float lerpSpeed;
    private Vector2 pushVector;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentRotation = bee.transform.eulerAngles.z;
        if (currentRotation > 180)
        {
            currentRotation = currentRotation - 360f;
        } 

        RotateAndTransform(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), currentRotation);

        timer += Time.deltaTime;
        if (timer >= 2.0) {
            pollen -= pollenLossRate;
            if (pollen < 0) {
                pollen = 0;
            }
            timer = 0.0;
        }

        lerpSpeed = 3f * Time.deltaTime;

        PollenBarSmoothFill();
        
    }

    void PollenBarSmoothFill() {
        pollenBar.fillAmount = Mathf.Lerp(pollenBar.fillAmount, (float)pollen / (float)pollenMax, lerpSpeed);
    }

    private void RotateAndTransform(float vertical, float horizontal, float currentRotation) 
    {
        
        if (horizontal == 0f && vertical == 0f)
        {
            rb2D.MoveRotation(currentRotation);
            rb2D.velocity = Vector2.zero;
            return;
        }

        float rotationDirection;
        float currentRad = currentRotation * Mathf.Deg2Rad;

        Vector2 moveVector = new Vector2(horizontal, vertical);
        Vector2 towardsVector = new Vector2(-Mathf.Sin(currentRad), Mathf.Cos(currentRad));
        Vector2 pushForce = Vector2.zero;

        if (numTimesOutOfBounds > 0)
        {
            timeInsideBoundary += Time.deltaTime;
            pushForce = pushVector * speed / 2f * (1f + timeInsideBoundary);
        }

        float rotationAngle = Vector2.Angle(Vector2.up, moveVector);

        if (MathF.Abs(currentRotation) < 10f)  
        {
            rotationDirection = horizontal > 0 ? -1f : 1f;
        }

        if (horizontal > 0)
        {
            rotationAngle = -rotationAngle;
        }

        if (Vector2.Angle(towardsVector, moveVector) > 150f)
        {
            rb2D.MoveRotation(currentRotation);
            rb2D.velocity = -speed * towardsVector.normalized + pushForce;
            return;
        }

        if (MathF.Abs(rotationAngle - currentRotation) < 15f)
        {
            rotationDirection = 0f;
        }
        
        if (rotationAngle > 0)
        {
            rotationDirection = (rotationAngle - 180f <= currentRotation && currentRotation <= rotationAngle) ? 1f : -1f;
        }
        else
        {
            rotationDirection = (rotationAngle <= currentRotation && currentRotation <= rotationAngle + 180f)? -1f : 1f;
        }

        if (Vector2.Angle(towardsVector, moveVector) > 4.0f)
        {
            rb2D.MoveRotation(currentRotation + rotationSpeed * rotationDirection);
        }
        
        rb2D.velocity = speed * towardsVector.normalized + pushForce;

    }


    void OnTriggerEnter2D(Collider2D other) 
    {
        switch (other.tag)
        {
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
                health = 3;
                break;
            case "Flower":
                pollen += pollenRate;
                if (pollen > pollenMax) {
                    pollen = pollenMax;
                }
                Debug.Log(pollen);
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

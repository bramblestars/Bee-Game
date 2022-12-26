using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float speed = 0.1f;
    [SerializeField] float rotationSpeed = 0.4f;
    [SerializeField] GameObject car;
    bool rotatingCounterClockwise;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float currentRotation = car.transform.eulerAngles.z;
        if (currentRotation > 180)
        {
            currentRotation = currentRotation - 360f;
        } 

        RotateAndTransform(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), currentRotation);
        
    }

    private void RotateAndTransform(float vertical, float horizontal, float currentRotation) 
    {
        if (horizontal == 0f && vertical == 0f)
        {
            return;
        }

        float rotationDirection;

        Vector3 moveVector = Vector3.zero;
        
        moveVector.x = horizontal;
        moveVector.y = vertical;

        float rotationAngle = Vector3.Angle(Vector3.up, moveVector);

        if (MathF.Abs(currentRotation) < 10f)  
        {
            rotationDirection = horizontal > 0 ? -1f : 1f;
        }

        if (horizontal > 0)
        {
            rotationAngle = -rotationAngle;
        }

        if (isGoingBackward(currentRotation, rotationAngle))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
            return;
        }

        if (MathF.Abs(rotationAngle - currentRotation) < 5f)
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

        transform.Rotate(0, 0, rotationSpeed * rotationDirection * Time.deltaTime);
        transform.Translate(0, speed * Time.deltaTime, 0);

    }

    private bool isGoingBackward(float currentAngle, float rotationAngle) 
    {
        float oppositeAngle = currentAngle - 180f;
        if (oppositeAngle < -180f)
        {
            oppositeAngle = oppositeAngle + 360f;
        }

        float upperLimit = oppositeAngle + 30f;
        float lowerLimit = oppositeAngle - 30f;

        Debug.Log("upper: " + upperLimit + " lower: " + lowerLimit + " current: " + currentAngle + " input: " + rotationAngle); 

        if (upperLimit > 180f)
        {
            return (-180f <= rotationAngle && rotationAngle <= upperLimit - 360f) || (lowerLimit <= rotationAngle && rotationAngle <= 180f);
        }
        else if (lowerLimit < -180f)
        {
            return (-180f <= rotationAngle && rotationAngle <= upperLimit) || (lowerLimit + 360f <= rotationAngle && rotationAngle <= 180f);
        }
        else 
        {
            return lowerLimit <= rotationAngle && rotationAngle <= upperLimit;
        }

    }
}

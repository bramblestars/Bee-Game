using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastShadow : MonoBehaviour
{

    [SerializeField] float shadowOffsetX = 1f;
    [SerializeField] float shadowOffsetY = 1f;
    [SerializeField] GameObject bee;
    Vector3 shadowOffset;
    float currentRotation;
    float relativeRotationRad;
    
    // Start is called before the first frame update
    void Start()
    {
        shadowOffset = new Vector3(shadowOffsetX, shadowOffsetY, 0f);

        transform.Translate(shadowOffsetX, shadowOffsetY, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation = bee.transform.eulerAngles.z;
        relativeRotationRad = currentRotation < 0? currentRotation + 120f : 120f - currentRotation;
        relativeRotationRad = relativeRotationRad * Mathf.Deg2Rad;

        transform.localPosition = new Vector3(shadowOffsetX  * Mathf.Sin(relativeRotationRad), -shadowOffsetY * Mathf.Cos(relativeRotationRad), 0f);
    }
}

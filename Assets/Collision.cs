using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] float destroyDelay = 0.5f;
    [SerializeField] Color32 hasPackageColor = new Color32(255, 255, 255, 255);
    [SerializeField] Color32 noPackageColor = new Color32(255, 255, 255, 255);
    [SerializeField] float boostSpeed = 2.0f;

    bool hasPackage;

    SpriteRenderer spriteRenderer;

    void Start() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = noPackageColor;
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log("Hello!!");    
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Package" && !hasPackage)
        {
            Debug.Log("Package picked up!");
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Destroy(other.gameObject, destroyDelay);
        }

        if (other.tag == "Customer" && hasPackage)
        {
            Debug.Log("Yay!");
            hasPackage = false;
            spriteRenderer.color = noPackageColor;
        }

        if (other.tag == "Boost")
        {

        }
    }
}

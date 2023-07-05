using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] float destroyDelay = 0.5f;
    [SerializeField] Color32 hasPollenColor = new Color32(255, 255, 255, 255);
    [SerializeField] Color32 noPollenColor = new Color32(255, 255, 255, 255);

    private Rigidbody2D rb2D;
    private bool hasPollen;

    SpriteRenderer spriteRenderer;

    void Start() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = noPollenColor;
        rb2D = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Flower" && !hasPollen)
        {
            Debug.Log("Yay flower!!");
            hasPollen = true;
            spriteRenderer.color = hasPollenColor;
            Destroy(other.GetComponent<CircleCollider2D>(), destroyDelay);
        }
    }

}

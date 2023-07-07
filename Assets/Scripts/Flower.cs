using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public Animator animator;
    private Collider2D flowerCollider;

    private bool timerRunning = false;
    private bool isVisible = false;
    private float timer = 0f;
    [SerializeField] float timerLimit = 10f;
    

    // Start is called before the first frame update
    void Start()
    {
        flowerCollider = GetComponent<Collider2D>();
    }

    void OnBecameVisible()
    {
        isVisible = false;
    }

    void OnBecameInvisible() {
        isVisible = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning && isVisible) {
            timer += Time.deltaTime;
            if (timer >= timerLimit) {
                timerRunning = false;
                timer = 0f;
                animator.SetBool("Touched", false);
                flowerCollider.enabled = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        animator.SetBool("Touched", true);
        timerRunning = true;
        flowerCollider.enabled = false;
    }
}

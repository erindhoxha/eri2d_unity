using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    public Animator animator;

    public float health = 3;
    public bool invincible = false;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!invincible) {
            if (health <= 0)
            {
                animator.SetTrigger("break");
                Invoke("DestroyBox", 0.5f);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fireball") {
            animator.SetTrigger("hit");
            health -= 2;
        }
        if (collision.gameObject.tag == "Sword")
        {
            animator.SetTrigger("hit");
            health--;
        }
    }

    void DestroyBox()
    {
        Destroy(gameObject);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private Health health;
    private Rigidbody2D rb;

    void Start() {
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (health.currentHealth == 0) return;
        target = target ?? PlayerController.instance.transform;
        if (target == null) return;
        Vector2 direction = target.position - transform.position;
        rb.AddForce(direction * 5);
    }
}

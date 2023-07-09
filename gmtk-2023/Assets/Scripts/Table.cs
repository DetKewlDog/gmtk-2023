using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    private Rigidbody2D rb;
    void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        if (other.TryGetComponent<Rigidbody2D>(out rb)) {
            rb.AddForce(1000 * Vector2.up);
            rb.gravityScale = 4;
            Invoke("ResetGravityScale", 0.4f);
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        if (other.TryGetComponent<Rigidbody2D>(out rb)) {
            rb.gravityScale = 4;
            Invoke("ResetGravityScale", 0.4f);
        }
    }

    void ResetGravityScale() {
        rb.gravityScale = 0;
        rb.velocity *= Vector2.right;
    }
}

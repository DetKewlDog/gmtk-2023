using UnityEngine;

class ThrowableHandler : MonoBehaviour
{
    public GameObject bloodEffect;
    bool triggered = false;
    void OnCollisionEnter2D(Collision2D other) {
        if (triggered || other.gameObject.CompareTag("Player")) return;
        var rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        Destroy(GetComponent<Collider2D>());
        transform.SetParent(other.transform);
        var effect = Instantiate(bloodEffect, transform);
        Vector2 direction = transform.position - other.transform.position;
        other.rigidbody.AddForce(direction * -750);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        effect.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        triggered = true;
    }
}
using UnityEngine;

class ThrowableHandler : MonoBehaviour
{
    public GameObject bloodEffect;
    bool triggered = false;
    void OnCollisionEnter2D(Collision2D other) {
        if (triggered) return;
        var rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        transform.SetParent(other.transform.parent);
        var effect = Instantiate(bloodEffect, transform);
        Vector2 direction = transform.position - other.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        effect.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        triggered = true;
    }
}
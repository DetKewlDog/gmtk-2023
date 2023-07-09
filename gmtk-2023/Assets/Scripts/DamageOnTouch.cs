using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    public LayerMask targetLayers;
    public int damageToApply = 1;
    private Health health;

    void Start() { }

    void OnTriggerEnter2D(Collider2D other) {
        if (!enabled) return;
        if (other.TryGetComponent<Health>(out health)
            && (targetLayers.value & (1 << other.gameObject.layer)) != 0) {
            health.Damage(damageToApply);
            Vector2 direction = (other.transform.position - transform.position).normalized;
            int knockbackMultipler = other.CompareTag("Player") ? 1000 : 100;
            other.GetComponent<Rigidbody2D>().AddForce(direction * knockbackMultipler);
        }
    }
    void OnTriggerStay2D(Collider2D other) => OnTriggerEnter2D(other);
}

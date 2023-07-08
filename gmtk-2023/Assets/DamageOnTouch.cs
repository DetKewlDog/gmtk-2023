using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    public LayerMask targetLayers;
    public int damageToApply = 1;
    private Health health;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<Health>(out health)
            && (targetLayers.value & (1 << other.gameObject.layer)) != 0) {
            health.Damage(damageToApply);
        }
    }
}

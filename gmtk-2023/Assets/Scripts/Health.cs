using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public float invCooldown;
    private bool canBeDamaged = true;

    public UnityEvent OnDeath;
    public UnityEvent OnRespawn;

    public void Damage(int damageAmount) {
        if (!canBeDamaged) return;
        canBeDamaged = false;
        currentHealth -= damageAmount;
        print($"took damage! {currentHealth} {maxHealth}");
        if (currentHealth <= 0) {
            Kill();
            return;
        }
        Invoke("EnableDamage", invCooldown);
    }

    public void Kill() {
        OnDeath.Invoke();
        print("L bozo you fucking died");
    }
    public void Respawn() {
        OnRespawn.Invoke();
    }

    void EnableDamage() => canBeDamaged = true;
}

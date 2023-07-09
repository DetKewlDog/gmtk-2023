using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public float invCooldown;
    private bool canBeDamaged = true;

    public UnityEvent OnDamage;
    public UnityEvent OnDeath;
    public UnityEvent OnRespawn;

    public void Damage(int damageAmount) {
        if (!canBeDamaged) return;
        canBeDamaged = false;
        currentHealth -= damageAmount;
        if (currentHealth <= 0) {
            Kill();
            return;
        }
        Invoke("EnableDamage", invCooldown);
        OnDamage.Invoke();
    }

    public void Kill() => OnDeath.Invoke();
    public void Respawn() => OnRespawn.Invoke();

    void EnableDamage() => canBeDamaged = true;
}

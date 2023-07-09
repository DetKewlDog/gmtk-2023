using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private Health health;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private NavMeshAgent agent;

    void Start() {
        // Physics2D.IgnoreCollision(PlayerController.instance.GetComponent<Collider2D>(), transform.GetChild(0).GetComponent<Collider2D>());
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = agent.updateUpAxis = false;
    }

    void FixedUpdate() {
        health = health ?? GetComponent<Health>();
        if (health.currentHealth <= 0) return;
        target = target ?? PlayerController.instance.transform;
        if (target == null) return;
        Vector2 direction = target.position - transform.position;
        sr.flipX = direction.x < 0;
        agent.SetDestination(target.position);
    }
}

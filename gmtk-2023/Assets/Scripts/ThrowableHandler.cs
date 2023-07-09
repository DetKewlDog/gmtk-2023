using UnityEngine;

class ThrowableHandler : MonoBehaviour
{
    public GameObject bloodEffect;
    public WeaponSO weapon;
    public AudioClip throwKillSFX;
    bool triggered = false;
    void OnTriggerEnter2D(Collider2D other) {
        print(other.gameObject.tag);
        if (triggered || !other.gameObject.CompareTag("Enemy")) return;
        var rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        GetComponent<Collider2D>().isTrigger = true;
        transform.SetParent(other.transform);
        var effect = Instantiate(bloodEffect, transform);
        Vector2 direction = transform.position - other.transform.position;
        other.GetComponent<Rigidbody2D>().AddForce(direction * -750);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        effect.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        var picker = gameObject.AddComponent<WeaponPicker>();
        picker.weapon = weapon;
        picker.Initialize();
        triggered = true;

        var audio = gameObject.AddComponent<AudioSource>();
        audio.clip = throwKillSFX;
        audio.Play();
    }
}
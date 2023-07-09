using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public GameObject bloodEffect;
    public WeaponSO currentWeapon;
    Camera mainCamera;
    GameObject currentWeaponGO;
    DamageOnTouch dot;
    bool isAttacking = false;

    float distanceFromPlayer = 1.25f;


    void Start() => mainCamera = Camera.main;

    void Update() {
        if (!currentWeaponGO) return;

        Vector2 direction = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        currentWeaponGO.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        currentWeaponGO.transform.localPosition = distanceFromPlayer * Vector3.one * direction.normalized;

        if (Input.GetKey(KeyCode.Mouse0)) {
            if (!isAttacking) StartCoroutine(Attack());
        }
        if (Input.GetKey(KeyCode.Mouse1)) {
            ThrowWeapon();
        }
    }

    System.Collections.IEnumerator Attack() {
        isAttacking = true;
        dot.enabled = true;
        distanceFromPlayer = 2.5f;
        var waitForEndOfFrame = new WaitForEndOfFrame();
        Vector2 scale = Vector2.one;
        for (float i = 0; i < 0.2f * 60; i++) {
            distanceFromPlayer = Mathf.Lerp(1.25f, 3.5f, i / (0.2f * 60));
            scale.x = Mathf.Lerp(1, 1.1f, i / (0.2f * 60));
            currentWeaponGO.transform.localScale = scale;
            yield return waitForEndOfFrame;
        }
        dot.enabled = false;
        for (float i = 0; i < 0.2f * 60; i++) {
            distanceFromPlayer = Mathf.Lerp(3.5f, 1.25f, i / (0.2f * 60));
            scale.x = Mathf.Lerp(1.1f, 1, i / (0.2f * 60));
            currentWeaponGO.transform.localScale = scale;
            yield return waitForEndOfFrame;
        }
        isAttacking = false;
    }

    public void EquipWeapon(WeaponSO weapon) {
        if (currentWeaponGO != null) Destroy(currentWeaponGO);
        currentWeapon = weapon;
        currentWeaponGO = new GameObject();
        currentWeaponGO.transform.SetParent(transform);

        currentWeaponGO.layer = LayerMask.NameToLayer("Weapon");

        var sr = currentWeaponGO.AddComponent<SpriteRenderer>();
        sr.sprite = weapon.sprite;
        sr.sortingOrder = 1;

        dot = currentWeaponGO.AddComponent<DamageOnTouch>();
        dot.damageToApply = weapon.damage;
        dot.targetLayers = 1 << LayerMask.NameToLayer("Enemy");
        dot.enabled = false;

        var collider = currentWeaponGO.AddComponent<BoxCollider2D>();

        var rb = currentWeaponGO.AddComponent<Rigidbody2D>();
        rb.mass = 0;
        rb.gravityScale = 0;
    }

    void ThrowWeapon() {
        currentWeaponGO.transform.SetParent(null);
        var rb = currentWeaponGO.GetComponent<Rigidbody2D>();
        Vector2 direction = (mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        rb.AddForce(Vector2.one * direction / 2);
        dot.enabled = true;
        dot.damageToApply *= 10;
        var handler = currentWeaponGO.AddComponent<ThrowableHandler>();
        handler.bloodEffect = bloodEffect;
        currentWeaponGO = null;
        currentWeapon = null;
    }
}

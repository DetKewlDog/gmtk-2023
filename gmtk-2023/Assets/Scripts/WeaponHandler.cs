using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public WeaponSO initialWeapon;
    public Material outlineMat;
    public GameObject bloodEffect;
    Camera mainCamera;
    GameObject currentWeapon;
    DamageOnTouch dot;
    bool isAttacking = false;

    float distanceFromPlayer = 1.25f;


    void Start() {
        mainCamera = Camera.main;
        EquipWeapon(initialWeapon);
    }

    void Update() {
        if (!currentWeapon) return;

        Vector2 direction = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        currentWeapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        currentWeapon.transform.localPosition = distanceFromPlayer * Vector3.one * direction.normalized;

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
            distanceFromPlayer = Mathf.Lerp(1.25f, 2.5f, i / (0.2f * 60));
            scale.x = Mathf.Lerp(1, 1.5f, i / (0.2f * 60));
            currentWeapon.transform.localScale = scale;
            yield return waitForEndOfFrame;
        }
        dot.enabled = false;
        for (float i = 0; i < 0.2f * 60; i++) {
            distanceFromPlayer = Mathf.Lerp(2.5f, 1.25f, i / (0.2f * 60));
            scale.x = Mathf.Lerp(1.5f, 1, i / (0.2f * 60));
            currentWeapon.transform.localScale = scale;
            yield return waitForEndOfFrame;
        }
        isAttacking = false;
    }

    void EquipWeapon(WeaponSO weapon) {
        currentWeapon = new GameObject();
        currentWeapon.transform.SetParent(transform);

        var sr = currentWeapon.AddComponent<SpriteRenderer>();
        sr.sprite = weapon.sprite;
        sr.material = outlineMat;

        dot = currentWeapon.AddComponent<DamageOnTouch>();
        dot.damageToApply = weapon.damage;
        dot.targetLayers = 1 << LayerMask.NameToLayer("Enemy");
        dot.enabled = false;

        var collider = currentWeapon.AddComponent<BoxCollider2D>();

        var rb = currentWeapon.AddComponent<Rigidbody2D>();
        rb.mass = 0;
        rb.gravityScale = 0;
    }

    void ThrowWeapon() {
        currentWeapon.transform.SetParent(null);
        var rb = currentWeapon.GetComponent<Rigidbody2D>();
        Vector2 direction = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rb.AddForce(Vector2.one * direction / 40);
        dot.enabled = true;
        dot.damageToApply *= 10;
        var handler = currentWeapon.AddComponent<ThrowableHandler>();
        handler.bloodEffect = bloodEffect;
        currentWeapon = null;
    }
}

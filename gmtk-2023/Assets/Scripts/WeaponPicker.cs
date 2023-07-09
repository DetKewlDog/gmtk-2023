using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPicker : MonoBehaviour
{
    public WeaponSO weapon;
    public Sprite pickedSprite;
    SpriteRenderer sr;
    public Material oldMaterial;
    public bool initialized = false;
    void Start() => Invoke("Initialize", 0.01f);
    public void Initialize() {
        sr = GetComponent<SpriteRenderer>();
        if (initialized || !weapon) return;
        oldMaterial = sr.material;
        if (sr.sprite == null) sr.sprite = weapon.sprite;
        initialized = true;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (!initialized) return;
        if (!other.CompareTag("Player")) return;
        sr.material = Resources.Load<Material>("Materials/PickerOutlineMat");
    }
    void OnTriggerExit2D(Collider2D other) {
        if (!initialized) return;
        if (!other.CompareTag("Player")) return;
        sr.material = oldMaterial;
    }
    void OnTriggerStay2D(Collider2D other) {
        if (!initialized) return;
        if (!other.CompareTag("Player")) return;
        if (Input.GetKeyDown(KeyCode.Space)) {
            var weaponHandler = other.GetComponent<WeaponHandler>();
            if (weaponHandler.currentWeapon != null) {
                var newPicker = Instantiate(gameObject, transform.position, Quaternion.identity).GetComponent<WeaponPicker>();
                newPicker.weapon = weaponHandler.currentWeapon;
                newPicker.Initialize();
                newPicker.oldMaterial = oldMaterial;
                newPicker.GetComponent<SpriteRenderer>().sprite = weaponHandler.currentWeapon.sprite;
            }
            weaponHandler.EquipWeapon(weapon);
            if (pickedSprite == null) Destroy(gameObject);
            else {
                sr.sprite = pickedSprite;
                sr.material = oldMaterial;
                Destroy(this);
            }
        }
    }
}

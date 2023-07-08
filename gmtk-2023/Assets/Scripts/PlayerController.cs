using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Rigidbody2D rb;
    public float velocity;

    private Vector2 movementVector;

    void Awake() => PlayerController.instance = this;
    void FixedUpdate() {
        movementVector =
            (Input.GetKey(KeyCode.D) ? Vector2.right : Vector2.zero) +
            (Input.GetKey(KeyCode.W) ? Vector2.up : Vector2.zero) -
            (Input.GetKey(KeyCode.S) ? Vector2.up : Vector2.zero) -
            (Input.GetKey(KeyCode.A) ? Vector2.right : Vector2.zero);

        bool isSliding = Input.GetKey(KeyCode.LeftShift);
        rb.drag = isSliding ? 0 : 5;
        int movementMultiplier = isSliding ? 3 : 3;

        rb.AddForce(movementVector * velocity * movementMultiplier);
    }
}

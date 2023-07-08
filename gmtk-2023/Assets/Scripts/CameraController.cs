using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 pos = new Vector3();
    void FixedUpdate() {
        pos = PlayerController.instance.transform.position;
        pos.z = -10;
        transform.position = Vector3.Slerp(transform.position, pos, 5 * Time.deltaTime);
    }
}

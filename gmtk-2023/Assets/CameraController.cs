using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 pos = new Vector3();
    void FixedUpdate() {
        pos = PlayerController.instance.transform.position;
        pos.z = -10;
        transform.position = pos;
    }
}

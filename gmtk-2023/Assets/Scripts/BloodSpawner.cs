using UnityEngine;

public class BloodSpawner : MonoBehaviour
{
    public GameObject BloodPuddle;
    public void Spawn() => Instantiate(BloodPuddle, transform.position - Vector3.up, Quaternion.identity);
}

using UnityEngine;

public class BloodSpawner : MonoBehaviour
{
    public GameObject BloodPuddle;
    public void Spawn() => Instantiate(BloodPuddle, transform.position - 2 * Vector3.up, Quaternion.identity);
}

using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    public SpriteRenderer sr;
    public Material NormalMat, FlashMat;
    public void Flash() {
        sr.material = FlashMat;
        Invoke("Unflash", 0.1f);
    }
    void Unflash() => sr.material = NormalMat;
}

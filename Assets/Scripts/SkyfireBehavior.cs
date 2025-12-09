using UnityEngine;

public class SkyfireBehavior : MonoBehaviour
{
    [Header("Ayarlar")]
    public float yukseklik = 10f; // Ne kadar yukarýdan düþsün
    public float ileriMesafe = 5f; // Kafamýza deðil, biraz ileriye düþsün

    void Start()
    {
        // 1. POZÝSYON DÜZELTME (Iþýnlanma)
        // Þu anki yerinden (elden) yukarý ve ileri taþýyoruz
        transform.position += (Vector3.up * yukseklik) + (transform.forward * ileriMesafe);

        // 2. YÖN DÜZELTME (Aþaðý Bakma)
        // Merminin yönünü aþaðý çeviriyoruz
        transform.rotation = Quaternion.LookRotation(Vector3.down);

        // 3. HAREKET YÖNÜNÜ GÜNCELLEME (Önemli!)
        // ProjectileBehavior scripti mermiyi "Setup" fonksiyonunda aldýðý yöne götürür.
        // Bizim o yönü "Aþaðý" olarak ezmemiz (override) lazým.

        ProjectileBehavior proj = GetComponent<ProjectileBehavior>();
        if (proj != null)
        {
            // DÝKKAT: ProjectileBehavior içindeki "direction" deðiþkeni public olmalý
            // veya yönü deðiþtiren bir fonksiyon yazmalýsýn.
            // Aþaðýdaki satýrýn çalýþmasý için ProjectileBehavior'da küçük bir ayar yapacaðýz.
            proj.SetDirection(Vector3.down);
        }
    }
}
using UnityEngine;
using System.Collections;

public class SkyfireBehavior : MonoBehaviour
{
    [Header("Settings")]
    public float damage = 50f;
    public float explosionRadius = 5f;
    public float warningDuration = 1.0f;

    [Header("Fall Animation")]
    public float dropHeight = 40f;
    public float spawnDistance = 5.0f; // Oyuncunun ne kadar önüne düþsün? (0.1 yerine 5 yaptýk)
    public float meteorScale = 4f;

    [Header("Visual References")]
    public GameObject warningCircle;
    public GameObject meteorVisual;
    public GameObject explosionEffect;

    void Start()
    {
        // Varsa eski projectile scriptini temizle
        ProjectileBehavior proj = GetComponent<ProjectileBehavior>();
        if (proj != null) Destroy(proj);

        // --- YÜKSEKLÝK AYARI KISMI (Buraya Odaklandýk) ---

        // 1. Hedefin X ve Z koordinatlarýný belirle (Oyuncunun baktýðý yönün ilerisi)
        Vector3 targetPos = transform.position + (transform.forward * spawnDistance);

        // 2. O noktanýn çok yukarýsýndan (Y=50) aþaðýya bir ýþýn (Raycast) at
        Vector3 rayOrigin = new Vector3(targetPos.x, 50f, targetPos.z);
        RaycastHit hit;

        // 3. Iþýn zemine çarparsa, hedefi o çarpýþma noktasýna taþý
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, 100f))
        {
            // hit.point = Zeminin tam üstü
            // +0.1f ekliyoruz ki görsel zeminin içine girip titremesin
            transform.position = hit.point + (Vector3.up * 0.5f);
        }
        else
        {
            // Eðer zemin bulunamazsa (harita dýþý vs.) varsayýlan yüksekliði kullan
            targetPos.y = 0.1f;
            transform.position = targetPos;
        }

        // Meteor boyutunu ayarla
        if (meteorVisual != null)
            meteorVisual.transform.localScale = Vector3.one * meteorScale;

        StartCoroutine(ExecuteSkyfire());
    }

    IEnumerator ExecuteSkyfire()
    {
        if (warningCircle != null) warningCircle.SetActive(true);
        if (meteorVisual != null) meteorVisual.SetActive(false);

        yield return new WaitForSeconds(warningDuration);

        if (warningCircle != null) warningCircle.SetActive(false);

        if (meteorVisual != null)
        {
            meteorVisual.SetActive(true);

            // Animasyon: Meteor yerden deðil, havadan (dropHeight kadar yukarýdan) düþsün
            Vector3 groundPos = transform.position; // Artýk transform.position tam zeminde
            Vector3 skyPos = groundPos + (Vector3.up * dropHeight);

            float fallTime = 0.2f;
            float timer = 0;

            while (timer < fallTime)
            {
                meteorVisual.transform.position = Vector3.Lerp(skyPos, groundPos, timer / fallTime);
                timer += Time.deltaTime;
                yield return null;
            }
            meteorVisual.transform.position = groundPos;
        }

        Explode();
    }

    void Explode()
    {
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                // Can scriptini bulmaya çalýþ
                HealthBar hpScript = hitCollider.GetComponent<HealthBar>();
                if (hpScript == null) hpScript = hitCollider.GetComponentInChildren<HealthBar>();

                if (hpScript != null)
                {
                    // --- BURAYI DEÐÝÞTÝRDÝK ---
                    // PlayerStats olmadýðý için þimdilik sadece sabit hasarý kullanýyoruz.
                    // Ýleride branchleri birleþtirince buraya "* PlayerStats.GlobalDamageMultiplier" eklersin.
                    hpScript.TakeDamage(damage);
                }
                // Debug.Log("Skyfire Hit Enemy: " + hitCollider.name);
            }
        }
        Destroy(gameObject, 0.5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
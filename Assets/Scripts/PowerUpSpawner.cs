using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs;

    [Header("Gerekli Baðlantýlar")]
    public Transform playerTransform; // YENÝ: Oyuncunun pozisyonunu buradan alacaðýz

    [Header("Spawn Ayarlarý")]
    public float spawnInterval = 2f; // Sýklýk (Test için hýzlý)

    // Artýk bu deðerler "Harita Boyutu" deðil, "Oyuncuya Olan Uzaklýk"
    [Tooltip("Oyuncunun ne kadar uzaðýnda (sað-sol) çýksýn?")]
    public float xDist = 10f;
    [Tooltip("Oyuncunun ne kadar uzaðýnda (ileri-geri) çýksýn?")]
    public float zDist = 10f;

    [Header("Raycast Ayarlarý")]
    public float rayStartHeight = 50f;
    public float heightOffset = 0.5f;

    void Start()
    {
        // Oyuncu atanmamýþsa hata vermesin diye kontrol
        if (playerTransform == null)
        {
            Debug.LogError("HATA: PowerUpSpawner scriptine Player objesini sürüklemeyi unuttun!");
            return;
        }

        InvokeRepeating("SpawnRandomPowerUp", 2f, spawnInterval);
    }

    void SpawnRandomPowerUp()
    {
        // Eðer oyuncu öldüyse ve yok olduysa hata vermesin, durdursun
        if (playerTransform == null) return;

        // 1. Oyuncunun o anki pozisyonunu al
        Vector3 playerPos = playerTransform.position;

        // 2. Oyuncunun etrafýnda rastgele bir mesafe belirle
        float randomXOffset = Random.Range(-xDist, xDist);
        float randomZOffset = Random.Range(-zDist, zDist);

        // 3. Iþýnýn baþlangýç noktasý = Oyuncunun Yeri + Rastgele Mesafe + Gökyüzü Yüksekliði
        Vector3 rayOrigin = new Vector3(
            playerPos.x + randomXOffset,
            rayStartHeight,
            playerPos.z + randomZOffset
        );

        RaycastHit hit;

        // 4. Iþýný at ve zemine çarparsa oluþtur
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, 100f))
        {
            Vector3 spawnPos = hit.point;
            spawnPos.y += heightOffset;

            int randomIndex = Random.Range(0, powerUpPrefabs.Length);
            Instantiate(powerUpPrefabs[randomIndex], spawnPos, Quaternion.identity);
        }
    }
}
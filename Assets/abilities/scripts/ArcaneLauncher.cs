

using UnityEngine;

public class ArcaneLauncher : MonoBehaviour
{
    [Header("Arcane Orb Settings")]
    public GameObject orbPrefab; // Drag the Purple Orb prefab here
    public float fireRate = 6f;  // Fires every 6 seconds (from GDD)

    private float timer = 0f;

    void Start()
    {
        // Optional: Set timer to fireRate so it shoots immediately upon starting
        // If you prefer it to wait 6 seconds first, remove this line.
        timer = fireRate;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            CastOrb();
            timer = 0f; // Reset timer
        }
    }

    void CastOrb()
    {
        if (orbPrefab != null)
        {
            // Spawn logic: 
            // 1.5m forward (so it doesn't clip inside player)
            // 1.5m up (approx chest/head height for a magic cast)
            Vector3 spawnPos = transform.position + (transform.forward * 1.5f) + (transform.up * 1.5f);

            // Instantiate the orb facing the same way as the player
            Instantiate(orbPrefab, spawnPos, transform.rotation);
        }
    }
}
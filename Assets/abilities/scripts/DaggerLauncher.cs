using UnityEngine;

public class DaggerLauncher : MonoBehaviour
{
    [Header("Settings")]
    public GameObject daggerPrefab; // Drag 'VFX_DaggerProjectile' here
    public float fireRate = 3f;     // Fires every 3 seconds
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            Fire();
            timer = 0f;
        }
    }

    void Fire()
    {
        if (daggerPrefab != null)
        {
            // Spawn Position: 1 meter in front, 1.5 meters up (Chest height)
            Vector3 spawnPos = transform.position + (transform.forward * 1.0f) + (Vector3.up * 1.5f);

            // Create the Slash
            Instantiate(daggerPrefab, spawnPos, transform.rotation);
        }
    }
}
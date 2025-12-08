using UnityEngine;

public class DaggerAbility : MonoBehaviour
{
    [Header("Settings")]
    public GameObject daggerPrefab; // Drag 'VFX_DaggerSlash' here
    public float fireRate = 3f;     // Fires every 3 seconds
    public float spawnHeight = 1.5f;// Height (Chest level)

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
            // Calculate spawn position: In front of player + up at chest height
            Vector3 spawnPos = transform.position + (transform.forward * 1.0f) + (Vector3.up * spawnHeight);

            // Spawn the dagger with the Player's rotation so it flies straight
            Instantiate(daggerPrefab, spawnPos, transform.rotation);
        }
    }
}
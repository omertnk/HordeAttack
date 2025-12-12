using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [Header("Settings")]
    public GameObject coinPrefab;

    [Range(0, 100)]
    public float dropChance = 50f;

    [Header("Bounce Settings")]
    public float jumpForce = 5f; // Power of the pop UP
    public float sideForce = 1f; // Power of the pop SIDEWAYS

    public void DropCoin()
    {
        float randomValue = Random.Range(0f, 100f);
        if (randomValue <= dropChance)
        {
            if (coinPrefab != null)
            {
                // Spawn slightly above the enemy
                Vector3 spawnPos = transform.position;
                spawnPos.y += 0.5f;

                GameObject droppedCoin = Instantiate(coinPrefab, spawnPos, Quaternion.identity);

                // ADD FORCE (The Bounce Logic)
                Rigidbody rb = droppedCoin.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Create a random direction so coins scatter slightly
                    float randomX = Random.Range(-1f, 1f);
                    float randomZ = Random.Range(-1f, 1f);
                    Vector3 forceDirection = new Vector3(randomX * sideForce, 1, randomZ * sideForce); // 1 is UP

                    // Apply the kick!
                    rb.AddForce(forceDirection * jumpForce, ForceMode.Impulse);
                }
            }
        }
    }
}
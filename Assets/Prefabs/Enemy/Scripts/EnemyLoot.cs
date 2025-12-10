using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [Header("Setti")]
    public GameObject coinPrefab; 
    
    [Range(0, 100)] 
    public float dropChance = 50f; 

    public void DropCoin()
    {
        float randomValue = Random.Range(0f, 100f);
        if (randomValue <= dropChance)
        {
            if (coinPrefab != null)
            {
                Vector3 spawnPos = transform.position;
                spawnPos.y += 0.5f; 

                GameObject droppedCoin = Instantiate(coinPrefab, spawnPos, Quaternion.identity);
                
                // Rigidbody rb = droppedCoin.GetComponent<Rigidbody>();
                // if (rb != null)
                // {
                //     rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
                // }
            }
        }
    }
}
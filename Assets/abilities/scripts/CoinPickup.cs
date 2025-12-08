using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 10; // How much this coin is worth

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the Manager on the player
            AbilityUnlockManager manager = other.GetComponent<AbilityUnlockManager>();

            if (manager != null)
            {
                manager.AddCoin(coinValue);

                // Optional: Play Sound or Particle Effect here

                Destroy(gameObject); // Remove coin
            }
        }
    }
}
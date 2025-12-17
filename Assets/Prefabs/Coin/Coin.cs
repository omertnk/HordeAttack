using UnityEngine;

public class Coin : MonoBehaviour
{
    public float xpValue = 10f;

    [Header("Effects")]
    public GameObject collectEffect;
    public AudioClip collectSound;

    // CHANGED: We use OnCollisionEnter because "Is Trigger" will be turned OFF.
    private void OnCollisionEnter(Collision collision)
    {
        // CHANGED: We check "collision.gameObject"
        if (collision.gameObject.CompareTag("Player"))
        {
            // 1. Give XP
            XpBar xpBarInstance = FindAnyObjectByType<XpBar>();
            if (xpBarInstance != null)
            {
                xpBarInstance.GainXp(xpValue);
            }

            // 2. Spawn Effect
            if (collectEffect != null)
            {
                Instantiate(collectEffect, transform.position, Quaternion.identity);
            }

            // 3. Play Sound
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            // 4. Destroy
            Destroy(gameObject);
        }
    }
}
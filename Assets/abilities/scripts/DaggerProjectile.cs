using UnityEngine;

public class DaggerProjectile : MonoBehaviour
{
    public float speed = 25f; // Fast speed from GDD
    public float lifetime = 2f; // Destroy after 2 seconds

    void Start()
    {
        // Destroy automatically so it doesn't clutter the game
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move FORWARD relative to its own rotation
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Dagger hit an enemy!");
            // TODO: Add damage logic here later

            // Destroy the dagger immediately on impact
            Destroy(gameObject);
        }
    }
}
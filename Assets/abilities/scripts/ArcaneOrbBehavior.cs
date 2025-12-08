using UnityEngine;

public class ArcaneOrbBehavior : MonoBehaviour
{
    public float speed = 5f; // Slow speed (from GDD)
    public float lifetime = 5f; // Lasts 5 seconds then vanishes

    void Start()
    {
        // Destroy automatically after 5 seconds
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move Forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Logic: Deal damage but DO NOT destroy the Orb.
            // It will keep flying and hit the next guy behind him.
            Debug.Log("Orb burned an enemy!");
        }
    }
}
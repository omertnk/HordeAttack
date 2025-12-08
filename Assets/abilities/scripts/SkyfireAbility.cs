using UnityEngine;
using System.Collections;

public class SkyfireAbility : MonoBehaviour
{
    [Header("Visuals")]
    public GameObject targetingCirclePrefab; // The Red Circle
    public GameObject explosionPrefab;       // The Meteor/Explosion

    [Header("Settings")]
    public float damageRadius = 3f;          // AoE Radius
    public float castDistance = 7f;          // How far in front to strike
    public float cooldown = 5f;              // Happens every 5 seconds
    public float delayBeforeImpact = 0.75f;  // Time between Circle and Boom

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= cooldown)
        {
            StartCoroutine(CastSkyfire());
            timer = 0f;
        }
    }

    IEnumerator CastSkyfire()
    {
        // 1. Calculate Position (Ground level in front of player)
        Vector3 targetPos = transform.position + (transform.forward * castDistance);
        targetPos.y = 0.1f; // Keep it slightly above ground to prevent flickering

        // 2. Spawn Targeting Circle
        if (targetingCirclePrefab != null)
        {
            GameObject circle = Instantiate(targetingCirclePrefab, targetPos, Quaternion.identity);
            Destroy(circle, delayBeforeImpact + 0.5f); // Cleanup circle shortly after impact
        }

        // 3. Wait for the meteor to "fall"
        yield return new WaitForSeconds(delayBeforeImpact);

        // 4. Spawn Explosion Visuals
        if (explosionPrefab != null)
        {
            GameObject boom = Instantiate(explosionPrefab, targetPos, Quaternion.identity);
            Destroy(boom, 2f); // Cleanup explosion effect
        }

        // 5. Deal Damage (Invisible Sphere)
        // Find all colliders inside the radius
        Collider[] hitEnemies = Physics.OverlapSphere(targetPos, damageRadius);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Debug.Log("Skyfire hit enemy: " + enemy.name);

                // TODO: Apply actual damage logic here later
                // Destroy(enemy.gameObject); 
            }
        }
    }

    // This draws a visible sphere in the Editor so you can see the blast radius
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 targetPos = transform.position + (transform.forward * castDistance);
        Gizmos.DrawWireSphere(targetPos, damageRadius);
    }
}
using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    // --- GLOBAL HASAR ÇARPANI (Bütün silahlar bunu okuyacak) ---
    public static float GlobalDamageMultiplier = 1f;

    [Header("Baðlantýlar")]
    public PlayerLocomotion locomotionScript;
    public HealthBar healthScript;

    void Start()
    {
        // Oyun her baþladýðýnda hasarý normale (1x) döndür
        GlobalDamageMultiplier = 1f;

        locomotionScript = GetComponent<PlayerLocomotion>();
        healthScript = Object.FindFirstObjectByType<HealthBar>();
    }

    // --- HIZ ---
    public void BoostSpeed(float multiplier, float duration)
    {
        if (locomotionScript != null) StartCoroutine(SpeedRoutine(multiplier, duration));
    }

    IEnumerator SpeedRoutine(float multiplier, float duration)
    {
        float originalSpeed = locomotionScript.movementSpeed;
        locomotionScript.movementSpeed *= multiplier;
        yield return new WaitForSeconds(duration);
        locomotionScript.movementSpeed = originalSpeed;
    }

    // --- CAN ---
    public void Heal(int amount)
    {
        if (healthScript != null) healthScript.Heal(amount);
        else
        {
            healthScript = Object.FindFirstObjectByType<HealthBar>();
            if (healthScript != null) healthScript.Heal(amount);
        }
    }

    // --- HASAR (DAMAGE) GÜÇLENDÝRMESÝ ---
    public void BoostDamage(float multiplier, float duration)
    {
        StartCoroutine(DamageRoutine(multiplier, duration));
    }

    IEnumerator DamageRoutine(float multiplier, float duration)
    {
        // 1. Hasarý arttýr (Örn: 1 iken 2 yap)
        GlobalDamageMultiplier = multiplier;
        Debug.Log(">>> HASAR GÜÇLENDÝ! Çarpan: " + GlobalDamageMultiplier);

        // 2. Süre bitene kadar bekle
        yield return new WaitForSeconds(duration);

        // 3. Hasarý normale döndür
        GlobalDamageMultiplier = 1f;
        Debug.Log(">>> Hasar normale döndü.");
    }
}
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealth = 100f;
    public float health;
    private float lerpSpeed = 0.01f;
    
    void Start()
    {
        health = maxHealth;
    }
    
    void Update()
    {
        if(!healthSlider || !easeHealthSlider) return;
        
        if (!Mathf.Approximately(healthSlider.value, health))
        {
            healthSlider.value = health;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }

        if (!Mathf.Approximately(healthSlider.value, easeHealthSlider.value))
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, healthSlider.value, lerpSpeed);
        }
    }

    void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
    }
}

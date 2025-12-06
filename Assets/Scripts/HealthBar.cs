using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealth = 100f;
    public float currentHealth;
    private float lerpSpeed = 0.01f;
    
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    void Update()
    {
        if(!healthSlider || !easeHealthSlider) return;
        
        if (!Mathf.Approximately(healthSlider.value, currentHealth))
        {
            healthSlider.value = currentHealth;
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
        currentHealth -= damageAmount;
    }
}

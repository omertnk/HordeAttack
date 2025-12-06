using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
    public Slider xpSlider;
    public Slider easeXpSlider;
    public float maxXp = 100f;
    public float xp;
    private float lerpSpeed = 0.01f;
    
    void Start()
    {
        xp = maxXp;
    }
    
    void Update()
    {
        if(!xpSlider || !easeXpSlider) return;
        
        if (!Mathf.Approximately(xpSlider.value, xp))
        {
            xpSlider.value = xp;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }

        if (!Mathf.Approximately(xpSlider.value, easeXpSlider.value))
        {
            easeXpSlider.value = Mathf.Lerp(easeXpSlider.value, xpSlider.value, lerpSpeed);
        }
    }

    void TakeDamage(float damageAmount)
    {
        xp -= damageAmount;
    }
}

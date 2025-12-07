using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
    public Slider xpSlider;
    public Slider easeXpSlider;
    public float maxXp = 100f;
    public float currentXp;
    private float lerpSpeed = 0.01f;
    
    public int characterLevel = 1;
    
    void Start()
    {
        currentXp = 0;
    }

    private void FixedUpdate()
    {
        if (currentXp > maxXp)
        {
            currentXp = 0;
            characterLevel++;
            Debug.Log("Level Up!");
        }
    }

    void Update()
    {
        if(!xpSlider || !easeXpSlider) return;
        
        if (!Mathf.Approximately(xpSlider.value, currentXp))
        {
            xpSlider.value = currentXp;
        }

        if (!Mathf.Approximately(xpSlider.value, easeXpSlider.value))
        {
            easeXpSlider.value = Mathf.Lerp(easeXpSlider.value, xpSlider.value, lerpSpeed);
        }
    }
    
    void GainXp(float xpAmount)
    {
        currentXp += xpAmount;
    }
}

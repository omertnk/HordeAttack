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
    
    private GameObject playerObj;
    
    void Start()
    {
        currentXp = 0;
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (currentXp > maxXp)
        {
            currentXp = 0;
            characterLevel++;
            AbilityController abilities = playerObj.GetComponent<AbilityController>();
            if(abilities != null)
            {
                abilities.UnlockAbility(characterLevel - 1); 
            }
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
    
    public void GainXp(float xpAmount)
    {
        currentXp += xpAmount;
    }
}

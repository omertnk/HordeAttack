using UnityEngine;

public class PlayerDamageReceiver : MonoBehaviour
{
    [Header("Health Scriptini Buraya Sürükle")]
    public HealthBar myHealthSystem; 

    public void TakeDamage(float amount)
    {
        if (myHealthSystem != null)
        {
            myHealthSystem.TakeDamage(amount); 
        }
       
    }
}

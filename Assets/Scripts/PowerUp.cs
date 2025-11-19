using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType { Speed, Damage, Health }

public class PowerUp : MonoBehaviour
{
    public PowerUpType type;
    public float duration = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats player = other.GetComponent<PlayerStats>();
            if (player != null)
            {
                switch (type)
                {
                    case PowerUpType.Speed:
                        StartCoroutine(player.ApplySpeedBoost(duration));
                        break;
                    case PowerUpType.Damage:
                        StartCoroutine(player.ApplyDamageBoost(duration));
                        break;
                    case PowerUpType.Health:
                        player.Heal(50); // Saðlýðý artýrýr
                        break;
                }
            }
            Destroy(gameObject);
        }
    }
}

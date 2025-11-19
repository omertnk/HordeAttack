using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public int coins = 0;
    public float speed = 5f;
    public float damage = 10f;
    public float health = 100f;

    void Awake()
    {
        instance = this;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log("Coins: " + coins);
    }

    public IEnumerator ApplySpeedBoost(float duration)
    {
        speed *= 2;
        yield return new WaitForSeconds(duration);
        speed /= 2;
    }

    public IEnumerator ApplyDamageBoost(float duration)
    {
        damage *= 2;
        yield return new WaitForSeconds(duration);
        damage /= 2;
    }

    public void Heal(float amount)
    {
        health += amount;
        if (health > 100) health = 100;
        Debug.Log("Health: " + health);
    }
}

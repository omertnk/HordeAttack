using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // kaç coin verdiðini ayarla

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Coin toplama
            PlayerStats.instance.AddCoins(coinValue);
            Destroy(gameObject); // Coin yok olur
        }
    }
}

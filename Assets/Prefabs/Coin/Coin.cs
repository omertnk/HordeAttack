using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public float xpValue = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("zart zurt");

            XpBar xpBarInstance = FindAnyObjectByType<XpBar>();

            xpBarInstance.GainXp(xpValue);

            Destroy(obj:this.gameObject);
        }
    }
}
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Hangi tür güçlendirme olduðunu seçmek için liste
    public enum PowerUpType { Speed, Damage, Health }
    public PowerUpType powerUpType;

    [Header("Ayarlar")]
    public float duration = 5f; // Etki süresi (Hýz ve Hasar için) 
    public float multiplier = 2f; // Kaç katýna çýkacaðý (2x)
    public int healthAmount = 25; // Ne kadar can vereceði

    public float disappearTime = 10f; // Yerden kaybolma süresi (toplanmazsa)

    void Start()
    {
        // Oyuncu bunu almazsa, belirlenen süre sonra kendi kendini yok et
        Destroy(gameObject, disappearTime);
    }

    // Bir obje buna deðdiðinde çalýþýr
    void OnTriggerEnter(Collider other)
    {
        // Çarpan obje "Player" etiketine sahip mi?
        if (other.CompareTag("Player"))
        {
            // Oyuncunun üzerindeki PlayerStats koduna ulaþ
            PlayerStats stats = other.GetComponent<PlayerStats>();

            if (stats != null)
            {
                // Türüne göre iþlem yap
                switch (powerUpType)
                {
                    case PowerUpType.Speed:
                        stats.BoostSpeed(multiplier, duration);
                        break;
                    case PowerUpType.Damage:
                        stats.BoostDamage(multiplier, duration);
                        break;
                    case PowerUpType.Health:
                        stats.Heal(healthAmount);
                        break;
                }
            }

            // Power-up toplandýktan sonra yok olmalý
            Destroy(gameObject);
        }
    }
}
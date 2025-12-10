using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("AYARLAR")]
    // YENİ ÖZELLİK: Bu kutucuğu işaretlersen kod seni oyuncu kabul eder!
    public bool isPlayer = false;

    [Header("UI References")]
    public Slider healthSlider;
    public Slider easeHealthSlider;

    [Header("Owner Settings")]
    public GameObject ownerObject;

    [Header("Stats")]
    public float maxHealth = 100f;
    public float currentHealth;
    private float lerpSpeed = 0.05f;

    private bool isDead = false;

    private Animator ownerAnimator;
    private Collider ownerCollider;
    private Rigidbody ownerRigidbody;

    void Start()
    {
        currentHealth = maxHealth;

        // Owner Object boşsa otomatik bul
        if (ownerObject == null)
        {
            ownerObject = transform.root.gameObject;
        }

        if (ownerObject != null)
        {
            ownerAnimator = ownerObject.GetComponent<Animator>();
            ownerCollider = ownerObject.GetComponent<Collider>();
            ownerRigidbody = ownerObject.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if (!healthSlider || !easeHealthSlider) return;

        if (!Mathf.Approximately(healthSlider.value, currentHealth))
        {
            healthSlider.value = currentHealth;
        }

        if (!Mathf.Approximately(healthSlider.value, easeHealthSlider.value))
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, healthSlider.value, lerpSpeed);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        if (ownerAnimator != null) ownerAnimator.enabled = false;
        if (ownerCollider != null) ownerCollider.enabled = false;

        // --- KRİTİK KONTROL ---
        // Artık Tag'e bakmıyoruz, direkt senin işaretlediğin kutucuğa bakıyoruz.
        if (isPlayer)
        {
            Debug.Log(">>> OYUNCU ÖLDÜ! (Game Over Başlatılıyor...)");

            // Karakterin fiziksel olarak düşmesi için (Rigidbody varsa)
            if (ownerRigidbody != null)
            {
                ownerRigidbody.constraints = RigidbodyConstraints.None;
                ownerRigidbody.AddTorque(transform.right * 10f);
            }

            // 2 saniye sonra Game Over ekranını tetikle
            Invoke("TriggerGameOver", 2f);
        }
        else
        {
            // Kutucuk işaretli değilse düşmandır
            Debug.Log("DÜŞMAN ÖLDÜ!");

            var agent = ownerObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (agent != null) agent.enabled = false;

            Destroy(ownerObject, 2f);
        }
    }

    void TriggerGameOver()
    {
        // GameManager'ı bul (Yeni ve Eski Unity sürümleri için uyumlu)
#if UNITY_2023_1_OR_NEWER
        GameManager gm = Object.FindFirstObjectByType<GameManager>();
#else
        GameManager gm = Object.FindObjectOfType<GameManager>();
#endif

        if (gm != null)
        {
            gm.GameOver();
        }
        else
        {
            Debug.LogError("HATA: Sahnede 'GameManager' scripti olan bir obje bulunamadı!");
        }
    }
}
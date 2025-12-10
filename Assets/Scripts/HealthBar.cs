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
        
        if (isPlayer)
        {
            Debug.Log(">>> OYUNCU ÖLDÜ! (Game Over Başlatılıyor...)");
            
            if (ownerRigidbody != null)
            {
                ownerRigidbody.constraints = RigidbodyConstraints.None;
                ownerRigidbody.AddTorque(transform.right * 10f);
            }
            
            Invoke("TriggerGameOver", 2f);
        }
        else
        {
            EnemyLoot lootScript = ownerObject.GetComponent<EnemyLoot>();
            
            if (lootScript != null)
            {
                lootScript.DropCoin();
            }

            var agent = ownerObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (agent != null) agent.enabled = false;

            Destroy(ownerObject, 2f);
        }
    }

    void TriggerGameOver()
    {
        GameManager gm = Object.FindFirstObjectByType<GameManager>();
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
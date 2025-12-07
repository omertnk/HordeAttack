using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("UI References")]
    public Slider healthSlider;
    public Slider easeHealthSlider;
    
    
    [Header("Stats")]
    public float maxHealth = 100f;
    public float currentHealth;
    private float lerpSpeed = 0.01f; 
    
    private bool isDead = false;
    
    private Animator animator;
    private InputManager inputManager;
    private Collider playerCollider;
    private GameObject playerObj;

    void Start()
    {
        currentHealth = maxHealth;
        
        playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            animator = playerObj.GetComponent<Animator>();
            playerCollider = playerObj.GetComponent<Collider>();
            inputManager = playerObj.GetComponent<InputManager>();
        }
        else
        {
            Debug.LogError("HATA: Sahne 'Player' tagine sahip bir obje yok!");
        }
    }
    
    void Update()
    {
        if(!healthSlider || !easeHealthSlider) return;
        
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
        Debug.Log("Karakter Öldü!");
        
        if (animator != null) 
            animator.enabled = false;
        
        playerObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        playerObj.GetComponent<Rigidbody>().AddTorque(transform.right * 10f);
        inputManager.enabled = false;
        
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }
    }
}
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private float damage;

    public void Setup(Vector3 _direction, float _damage, float _speed, float _lifeTime)
    {
        direction = _direction;
        damage = _damage;
        speed = _speed;
        
        Destroy(gameObject, _lifeTime);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter");
        if (other.CompareTag("Player")) 
        {
            return;
        }
        
        if (other.CompareTag("Enemy"))
        {
            HealthBar healthBar = other.GetComponentInChildren<HealthBar>();
            healthBar.TakeDamage(damage);

            if (healthBar.currentHealth <= 0)
            {
                Destroy(other.gameObject);
                Debug.Log(other.name);
            }
            
            Destroy(gameObject);
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Environment")) 
        {
            Destroy(gameObject);
        }
    }
}
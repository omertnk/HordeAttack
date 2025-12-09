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
            other.GetComponentInChildren<HealthBar>().TakeDamage(damage);
            
            Destroy(gameObject);
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Environment")) // Veya Tag ile
        {
            Destroy(gameObject);
        }
    }
}
using UnityEngine;
using UnityEngine.AI; 

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;

    [Header("Savaş Ayarları")]
    public float damage = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f; 
    private float lastAttackTime;

    [Header("Dönüş Ayarı")]
    public float turnSpeed = 5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        UpdateAnimations();
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.isStopped = true;
            
            FaceTarget(); 

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                StartAttackAnimation();
                lastAttackTime = Time.time;
            }
        }
    }
    
    void StartAttackAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }
    
    public void DealDamageToPlayer()
    {
        if(player == null) return;
        
        float distance = Vector3.Distance(transform.position, player.position);
        
        if (distance <= attackRange + 1.0f) 
        {
            PlayerDamageReceiver receiver = player.GetComponent<PlayerDamageReceiver>();
            if (receiver != null)
            {
                receiver.TakeDamage(damage);
            }
        }
    }
    
    void UpdateAnimations()
    {
        if (animator == null) return;
        
        bool isMoving = agent.velocity.magnitude > 0.1f;
        
        animator.SetBool("isRunning", isMoving);
    }
    
    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        
        direction.y = 0;
        
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }
    }
    
}
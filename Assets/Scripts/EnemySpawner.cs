using UnityEngine;
using UnityEngine.AI; 
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Types")]
    public GameObject[] enemyPrefabs;
    
    public GameObject player;       
    
    [Header("Spawn Settings")]
    public float spawnRadius = 20f;   
    public float spawnInterval = 3f;  

    void Start()
    {
        StartCoroutine(SpawnRoutine());
        Debug.Log("StartCoroutine Called");
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || player == null) return;
        
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        
        Vector3 randomPos = new Vector3(randomCircle.x, 0, randomCircle.y) + player.transform.position;
        
        randomPos.y = 100f; 

        NavMeshHit hit;
        
        if (NavMesh.SamplePosition(randomPos, out hit, 200.0f, NavMesh.AllAreas))
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject selectedEnemy = enemyPrefabs[randomIndex];
            
            Instantiate(selectedEnemy, hit.position, Quaternion.identity);
        }
    }
}
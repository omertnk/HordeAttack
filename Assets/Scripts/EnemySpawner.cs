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
        if (enemyPrefabs.Length == 0) return;
        
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject selectedEnemy = enemyPrefabs[randomIndex];
        
        Vector3 randomPoint = Random.insideUnitSphere * spawnRadius;
        randomPoint += player.transform.position;
        randomPoint.y = 0;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 10.0f, NavMesh.AllAreas))
        {
            Instantiate(selectedEnemy, hit.position, Quaternion.identity);
        }
    }
}
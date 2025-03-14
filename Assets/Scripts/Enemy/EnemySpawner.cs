using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab1;
    [SerializeField] private GameObject enemyPrefab2;
    [SerializeField] private GameObject enemyPrefab3;
    [SerializeField] private GameObject enemyPrefab4;
    [SerializeField] private GameObject enemyPrefab5;
    [SerializeField] private GameObject enemyPrefab6;
    // [SerializeField] private float changeRateOfEachEnemy; // Set different for each enemy (for multi-enemy)
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;

    [SerializeField] private float minRespawnDelay = 2f; // Minimum delay before respawning
    [SerializeField] private float maxRespawnDelay = 5f; // Maximum delay before respawning

    [SerializeField] private Transform spawn1;
    [SerializeField] private Transform spawn2;
    [SerializeField] private Transform spawn3;
    [SerializeField] private Transform spawn4;
    [SerializeField] private Transform spawn5;
    [SerializeField] private Transform spawn6;

    private bool isSpawn1Occupied = false; // Check there is enemy in the each platform or not
    private bool isSpawn2Occupied = false;
    private bool isSpawn3Occupied = false;
    private bool isSpawn4Occupied = false;
    private bool isSpawn5Occupied = false;
    private bool isSpawn6Occupied = false;

    private float timeUntilSpawn;

    void Awake()
    {
        SetTimeUntilSpawn(); // Set the time at the scene first loads 
        
    }

    void Update()
    {
        timeUntilSpawn -= Time.deltaTime; // Reduce the time 

        if (timeUntilSpawn < 0)
        {

            if (!isSpawn1Occupied)
            {
                InstantiateEnemyByTag("EnemySpawn1");
                isSpawn1Occupied = true;
            }
            else if (!isSpawn2Occupied)
            {
                InstantiateEnemyByTag("EnemySpawn2");
                isSpawn2Occupied = true;
            }
            else if (!isSpawn3Occupied)
            {
                InstantiateEnemyByTag("EnemySpawn3");
                isSpawn3Occupied = true;
            }
            else if (!isSpawn4Occupied)
            {
                InstantiateEnemyByTag("EnemySpawn4");
                isSpawn4Occupied = true;
            }
            else if (!isSpawn5Occupied)
            {
                InstantiateEnemyByTag("EnemySpawn5");
                isSpawn5Occupied = true;
            }
            else if (!isSpawn6Occupied)
            {
                InstantiateEnemyByTag("EnemySpawn6");
                isSpawn6Occupied = true;
            }

            // Reset spawn timer
            SetTimeUntilSpawn();

        }
    }

    public void InstantiateEnemyByTag(string enemyTag)
    {
        GameObject enemyToSpawn = null;
        Transform spawnPosition = null;

        // Determine which enemy prefab and spawn position to use
        switch (enemyTag)
        {
            case "EnemySpawn1":
                enemyToSpawn = enemyPrefab1;
                spawnPosition = spawn1;
                break;

            case "EnemySpawn2":
                enemyToSpawn = enemyPrefab2;
                spawnPosition = spawn2;
                break;
            case "EnemySpawn3":
                enemyToSpawn = enemyPrefab3;
                spawnPosition = spawn3;
                break;
            case "EnemySpawn4":
                enemyToSpawn = enemyPrefab1;
                spawnPosition = spawn4;
                break;

            case "EnemySpawn5":
                enemyToSpawn = enemyPrefab2;
                spawnPosition = spawn5;
                break;
            case "EnemySpawn6":
                enemyToSpawn = enemyPrefab3;
                spawnPosition = spawn6;
                break;

            default:
                Debug.LogWarning("No matching enemy tag for: " + enemyTag);
                return;
        }

        // Instantiate the enemy at the determined spawn position
        if (enemyToSpawn != null && spawnPosition != null)
        {
            Instantiate(enemyToSpawn, spawnPosition.position, Quaternion.identity);
            //Debug.Log("Spawned " + enemyTag + " at " + spawnPosition.position);
        }
    }
    public void ResetSpawnAvailability(string enemyTag)
    {
        // Start a delayed coroutine to reset spawn availability
        StartCoroutine(DelayedResetSpawn(enemyTag));
    }

    private IEnumerator DelayedResetSpawn(string enemyTag)
    {
        float delay = Random.Range(minRespawnDelay, maxRespawnDelay);
        //Debug.Log("Delaying respawn " + delay + " seconds for " + enemyTag);

        yield return new WaitForSeconds(delay); // Wait for the delay duration

        // Reset spawn availability for the specified tag
        switch (enemyTag)
        {
            case "EnemySpawn1":
                isSpawn1Occupied = false;
                break;

            case "EnemySpawn2":
                isSpawn2Occupied = false;
                break;

            case "EnemySpawn3":
                isSpawn3Occupied = false;
                break;
            case "EnemySpawn4":
                isSpawn4Occupied = false;
                break;
            case "EnemySpawn5":
                isSpawn5Occupied = false;
                break;
            case "EnemySpawn6":
                isSpawn6Occupied = false;
                break;

            default:
                Debug.LogWarning("No matching spawn tag for reset: " + enemyTag);
                break;
        }
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
        //Debug.Log("Next enemy spawn in: " + timeUntilSpawn + " seconds");
    }

}

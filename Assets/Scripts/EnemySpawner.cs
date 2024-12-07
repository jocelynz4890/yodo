using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float GameTimer = 0f;
    [SerializeField] private float TimeBetweenSpawn = 10f;
    [SerializeField] private int EnemiesSpawned = 5;
    [SerializeField] public bool IsPaused = false;
    public GameObject WeakZombiePrefab;
    private List<SpawnPoint> spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        //Create a list of spawnpoint objects
        spawnPoints = new List<SpawnPoint>(GameObject.FindObjectsOfType<SpawnPoint>());
    }

    void SpawnZombie(GameObject ZombieToSpawn)
    {
        //check that spawnpoints exist
        if (spawnPoints.Count == 0) return;
        //Choose a random spawnpoint, and spawn a zombie at that point.
        SpawnPoint randomPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        Instantiate(ZombieToSpawn, randomPoint.transform.position, randomPoint.transform.rotation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsPaused) {
            GameTimer += Time.fixedDeltaTime;
            if (GameTimer >= TimeBetweenSpawn)
            {
                //Spawns zombies intermittently
                for (int i = 0; i < EnemiesSpawned; i++) {
                    SpawnZombie(WeakZombiePrefab);
                }
                GameTimer = 0f;
            }
        }
    }
}

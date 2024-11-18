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

    List<Vector3> vector3List = new List<Vector3>
        {
            new Vector3(1.0f, 5f, 3.0f),
            new Vector3(4.0f, 5f, 6.0f),
            new Vector3(7.0f, 5f, 9.0f)
        };
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = new List<SpawnPoint>(GameObject.FindObjectsOfType<SpawnPoint>());
    }

    void SpawnZombie(GameObject ZombieToSpawn)
    {
        if (spawnPoints.Count == 0) return;

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
                for (int i = 0; i < EnemiesSpawned; i++) {
                    SpawnZombie(WeakZombiePrefab);
                }
                GameTimer = 0f;
                Debug.Log("Zombie Spawned, Timer Reset");
            }
        }
    }
}

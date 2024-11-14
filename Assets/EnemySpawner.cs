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

    List<Vector3> vector3List = new List<Vector3>
        {
            new Vector3(1.0f, 5f, 3.0f),
            new Vector3(4.0f, 5f, 6.0f),
            new Vector3(7.0f, 5f, 9.0f)
        };
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(WeakZombiePrefab, new Vector3(110, 10, 10), Quaternion.identity);
    }

    void SpawnZombie(GameObject ZombieToSpawn, Vector3 position)
    {
        Instantiate(ZombieToSpawn, position, Quaternion.identity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsPaused) {
            GameTimer += Time.fixedDeltaTime;
            if (GameTimer >= TimeBetweenSpawn)
            {
                SpawnZombie(WeakZombiePrefab, new Vector3(-30, 5, -70));
                GameTimer = 0f;
                Debug.Log("Zombie Spawned, Timer Reset");
            }
        }
    }
}

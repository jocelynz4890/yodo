using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    
    [Header("Spawn Area")]
    public Vector3 center = new Vector3(350, 1, -25);
    public Vector3 areaSize = new Vector3(10, 0, 10); 
    void Start()
    {
        SpawnPrefabs();
    }

    [ContextMenu("Spawn")]
    void SpawnPrefabs()
    {
        if (itemPrefabs.Length == 0)
        {
            Debug.LogWarning("No weapon prefabs assigned to the spawner.");
            return;
        }

        foreach (GameObject prefab in itemPrefabs)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(center.x - areaSize.x / 2, center.x + areaSize.x / 2),
                center.y,
                Random.Range(center.z - areaSize.z / 2, center.z + areaSize.z / 2)
            );
            
            var collectible = Instantiate(prefab, randomPosition, prefab.transform.rotation);
            collectible.transform.gameObject.tag = "CanPickUp";
            collectible.transform.GetChild(0).gameObject.tag = "CanPickUp";
        }
    }
    
    // Draw the spawn area in the Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 0f, 0f);
        Gizmos.DrawCube(center, areaSize);
    }
}

using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public TMP_Text woodCountText; // Assign in Inspector (TextMeshPro for Wood Count)
    public TMP_Text stoneCountText; // Assign in Inspector (TextMeshPro for Stone Count)

    private int woodCount = 0;
    private int stoneCount = 0;

    private void Start()
    {
        UpdateUI(); // Initialize UI with starting resource counts
    }

    public void AddResource(string resourceType, int amount)
    {
        if (resourceType.ToLower() == "tree")
        {
            woodCount += amount;
        }
        else if (resourceType.ToLower() == "rock")
        {
            stoneCount += amount;
        }
        else
        {
            Debug.LogWarning($"Unknown resource type: {resourceType}");
            return;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        woodCountText.text = "Wood: " + woodCount;
        stoneCountText.text = "Stone: " + stoneCount;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class BuildingPlacement : MonoBehaviour
{

    [System.Serializable]
    public class BuildingObject
    {
        public string buildingName;
        public GameObject prefab;
        public Vector3 rotationOffset = Vector3.zero;
        public float placementDistance = 2f;
        public int materialCost = 1;
    }

    [Header("Building Prefabs")]
    [SerializeField] private List<BuildingObject> availableBuildings = new List<BuildingObject>();
    [SerializeField] private int selectedBuildingIndex = 0;

    [Header("Requirements")]
    [SerializeField] private bool hasToolbox = false;
    [SerializeField] private bool hasMaterials = false;
    
    [Header("Placement Settings")]
    [SerializeField] private bool showPlacementPreview = true;
    [SerializeField] private Material previewMaterial;
    //Preview Settings
    private GameObject previewObject;
    private const string PLACEABLE_TAG = "PlacableTerrain";

    [Header("Player Settings")]
    [SerializeField] private PlayerInput playerInput;  // Reference to Unity's PlayerInput component
    private PlayerController playerController;
    private Camera playerCamera;  // Camera specific to this player

    private float placementCooldown = 0f;

    private void Awake()
    {
        // Get the camera assigned to this player (you'll need to set this up in your scene)
        playerCamera = GetComponentInChildren<Camera>();
        if (playerCamera == null)
            playerCamera = Camera.main;  // Fallback to main camera if no specific camera is found

        SetupInputActions();

        playerController = GetComponent<PlayerController>();
    }

    private void SetupInputActions()
    {
        // Assuming you're using the new Input System with PlayerInput component
        if (playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>();
        }
    }

    private void OnDestroy()
    {
        if (previewObject != null)
        {
            Destroy(previewObject);
        }
    }
    
    private void Update()
    {
        if (placementCooldown > 0f)
        {
            placementCooldown -= Time.deltaTime;
            if (previewObject != null)
            {
                previewObject.SetActive(false);
            }
        }

        if (showPlacementPreview && CanPlace() && placementCooldown <= 0f)
        {
            UpdateBuildingPreview();
        }

        if (CanPlace() && playerController.fire && placementCooldown <= 0f)
        {
            Vector3 position;
            Quaternion rotation;
            if (GetPlacementPosition(out position, out rotation))
            {
                PlaceBuilding(position, rotation);
                placementCooldown = 5f;
            }
        }
    }
    
    private void UpdateBuildingPreview()
    {
        if (hasToolbox && previewObject == null && GetSelectedBuilding()?.prefab != null)
        {
            previewObject = Instantiate(GetSelectedBuilding().prefab);
            SetPreviewMaterial(previewObject);
        }

        if (previewObject != null)
        {
            Vector3 position;
            Quaternion rotation;
            if (GetPlacementPosition(out position, out rotation))
            {
                previewObject.transform.position = position;
                previewObject.transform.rotation = rotation;
                previewObject.SetActive(true);
            }
            else
            {
                previewObject.SetActive(false);
            }

        }
    }


    private void SetPreviewMaterial(GameObject preview)
    {
        var renderers = preview.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            renderer.material = previewMaterial;
        }

        var colliders = preview.GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }
    
    private bool CanPlace()
    {
        return hasToolbox && 
               hasMaterials && 
               selectedBuildingIndex < availableBuildings.Count &&
               GetSelectedBuilding()?.prefab != null;
    }
    
    private BuildingObject GetSelectedBuilding()
    {
        if (selectedBuildingIndex >= 0 && selectedBuildingIndex < availableBuildings.Count)
        {
            return availableBuildings[selectedBuildingIndex];
        }
        return null;
    }

    private RaycastHit[] hits = new RaycastHit[8]; // Cache this array as a field to avoid allocation

    private bool GetPlacementPosition(out Vector3 position, out Quaternion rotation)
    {
        position = Vector3.zero;
        rotation = Quaternion.identity;

        BuildingObject selectedBuilding = GetSelectedBuilding();
        if (selectedBuilding == null) return false;

        // 1. Calculate the starting point for our raycast
        Vector3 forward = Vector3.ProjectOnPlane(playerCamera.transform.forward, Vector3.up).normalized;
        Vector3 placementPoint = transform.position + (forward * selectedBuilding.placementDistance);

        // 2. Start raycast from above the point to handle uneven terrain
        Vector3 rayStart = placementPoint + Vector3.up * 10f;

        // 3. Use RaycastNonAlloc for better performance
        int hitCount = Physics.RaycastNonAlloc(
            rayStart,          // Start position
            Vector3.down,      // Direction (downward)
            hits,             // Pre-allocated array for results
            20f              // Maximum distance to check
        );

        // 4. Find the highest valid placement point
        float highestPoint = float.NegativeInfinity;
        bool foundPlaceable = false;

        for (int i = 0; i < hitCount; i++)
        {
            // 5. Check if this hit point is on placeable terrain
            if (hits[i].collider.CompareTag(PLACEABLE_TAG) ||
                (hits[i].collider.transform.parent != null && hits[i].collider.transform.parent.CompareTag(PLACEABLE_TAG)))
            {
                // 6. Keep track of the highest valid point
                if (hits[i].point.y > highestPoint)
                {
                    position = hits[i].point;
                    highestPoint = hits[i].point.y;
                    foundPlaceable = true;
                }
            }
        }

        if (foundPlaceable)
        {
            // 7. Set rotation based on camera direction
            rotation = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0) *
                      Quaternion.Euler(selectedBuilding.rotationOffset);
            return true;
        }

        return false;
    }

    private void PlaceBuilding(Vector3 position, Quaternion rotation)
    {
        BuildingObject selectedBuilding = GetSelectedBuilding();
        if (selectedBuilding == null) return;
        
        GameObject newBuilding = Instantiate(selectedBuilding.prefab, position, rotation);
        OnBuildingPlaced(newBuilding, selectedBuilding);
    }
    
    protected virtual void OnBuildingPlaced(GameObject placedObject, BuildingObject buildingData)
    {
        // Override this method to implement additional functionality
    }
    
    public void SelectBuilding(int index)
    {
        if (index >= 0 && index < availableBuildings.Count)
        {
            selectedBuildingIndex = index;
            if (previewObject != null)
            {
                Destroy(previewObject);
                previewObject = null;
            }
        }
    }
    
    public void SelectBuilding(string buildingName)
    {
        int index = availableBuildings.FindIndex(b => b.buildingName == buildingName);
        if (index != -1)
        {
            SelectBuilding(index);
        }
    }
    
    public void AddBuilding(BuildingObject building)
    {
        if (building != null && building.prefab != null)
        {
            availableBuildings.Add(building);
        }
    }
    
    public void SetHasToolbox(bool value)
    {
        hasToolbox = value;
    }
    
    public void SetHasMaterials(bool value)
    {
        hasMaterials = value;
    }

    public bool getHasToolbox()
    {
        return hasToolbox;
    }
}
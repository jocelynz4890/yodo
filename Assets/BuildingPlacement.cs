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
    [SerializeField] private float previewUpdateInterval = 0.05f; // Update preview every 50ms
    
    private Camera mainCamera;
    private InputAction fireAction;
    private GameObject previewObject;
    private const string PLACEABLE_TAG = "PlacableTerrain";
    private float lastPreviewUpdate;
    
    private void Awake()
    {
        mainCamera = Camera.main;
        SetupInputActions();
    }
    
    private void SetupInputActions()
    {
        fireAction = new InputAction("Fire", InputActionType.Button);
        fireAction.AddBinding("<Mouse>/leftButton");
        fireAction.performed += OnFirePerformed;
        fireAction.Enable();
    }
    
    private void OnDestroy()
    {
        if (fireAction != null)
        {
            fireAction.performed -= OnFirePerformed;
            fireAction.Disable();
            fireAction.Dispose();
        }
        
        if (previewObject != null)
        {
            Destroy(previewObject);
        }
    }
    
    private void Update()
    {
        if (showPlacementPreview && CanPlace())
        {
            // Only update preview at fixed intervals
            if (Time.time - lastPreviewUpdate >= previewUpdateInterval)
            {
                UpdateBuildingPreview();
                lastPreviewUpdate = Time.time;
            }
        }
    }
    
    private void UpdateBuildingPreview()
    {
        if (previewObject == null && GetSelectedBuilding()?.prefab != null)
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
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                Material previewMaterial = new Material(materials[i]);
                previewMaterial.color = new Color(
                    previewMaterial.color.r,
                    previewMaterial.color.g,
                    previewMaterial.color.b,
                    0.5f
                );
                materials[i] = previewMaterial;
            }
            renderer.materials = materials;
        }
    }

    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        if (CanPlace())
        {
            Vector3 position;
            Quaternion rotation;
            if (GetPlacementPosition(out position, out rotation))
            {
                PlaceBuilding(position, rotation);
            }
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
    
    private bool GetPlacementPosition(out Vector3 position, out Quaternion rotation)
    {
        position = Vector3.zero;
        rotation = Quaternion.identity;
        
        BuildingObject selectedBuilding = GetSelectedBuilding();
        if (selectedBuilding == null) return false;

        // Calculate forward position from player, ignoring vertical component
        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0;
        forward.Normalize();
        
        // Get position in front of player
        Vector3 placementPoint = transform.position + (forward * selectedBuilding.placementDistance);
        
        // Cast a larger ray to better handle uneven terrain
        Ray ray = new Ray(placementPoint + Vector3.up * 10f, Vector3.down);
        RaycastHit[] hits = Physics.RaycastAll(ray, 20f);
        
        // Find the highest point that's tagged as placeable
        float highestPoint = float.NegativeInfinity;
        bool foundPlaceable = false;
        
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag(PLACEABLE_TAG) && hit.point.y > highestPoint)
            {
                position = hit.point;
                highestPoint = hit.point.y;
                foundPlaceable = true;
            }
        }
        
        if (foundPlaceable)
        {
            // Calculate rotation based on camera's Y rotation only
            float yRotation = mainCamera.transform.eulerAngles.y;
            rotation = Quaternion.Euler(0, yRotation, 0) * Quaternion.Euler(selectedBuilding.rotationOffset);
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
}
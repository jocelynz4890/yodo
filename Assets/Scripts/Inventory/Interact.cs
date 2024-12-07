using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    public InventorySystem inventorySystem;
    private PlayerInput _playerInput;

    public void Awake()
    {
        inventorySystem = GetComponent<InventorySystem>();
        if (inventorySystem == null)
        {
            Debug.LogError("InventorySystem not found on player.");
        }
        _playerInput = GetComponent<PlayerInput>();
    }
    
    void OnEnable()
    {
        if (_playerInput != null)
        {
            _playerInput.onActionTriggered += OnAction;
        }
    }

    void OnDisable()
    {
        if (_playerInput != null)
        {
            _playerInput.onActionTriggered -= OnAction;
        }
    }

    void OnDestroy()
    {
        if (_playerInput != null)
        {
            _playerInput.onActionTriggered -= OnAction;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CanPickUp"))
        {
            ItemObject item = other.GetComponent<ItemObject>();
            if (item != null && inventorySystem.weapon == null)
            {
                item.OnHandlePickupItem(inventorySystem);
                Destroy(other.gameObject);
            }
        }
    }
    
    void OnAction(InputAction.CallbackContext context)
    {
        // switch (context.action.name)
        // {
        //     case "Drop":
        //         Debug.Log($"Attempting to drop {inventorySystem.weapon.displayName}");
        //         inventorySystem.RemoveWeapon(inventorySystem.weapon);
        //         inventorySystem.Unequip();
        //         break;
        // }
    }
}

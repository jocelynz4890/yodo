using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string itemName;

    public string GetItemName()
    {
        return itemName;
    }
}

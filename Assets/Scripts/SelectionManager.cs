using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public GameObject SelectionText; // The UI Text object for showing interaction text
    public GameObject crosshair; // The crosshair UI Image
    private TextMeshProUGUI interaction_text;
    public bool onTarget; // track to make sure cursor is on object, not its collider
    public bool isPlayer1;

    private void Start()
    {
        // Initialize the interaction text component
        interaction_text = SelectionText.GetComponent<TextMeshProUGUI>();

        // Find the crosshair UI element if not assigned
        if (crosshair == null)
        {
            crosshair = GameObject.Find("Crosshair");
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            // Check if the hit object has the InteractableObjectName component
            var interactableObject = selectionTransform.GetComponent<InteractableObjectName>();
            if (interactableObject && ((isPlayer1 && interactableObject.player1InRange) || (!isPlayer1 && interactableObject.player2InRange)) )
            {
                onTarget = true;
                // Update the interaction text
                interaction_text.text = interactableObject.GetItemName();

                // Change the crosshair color based on the interaction
                if (interaction_text.text == "Zombie")
                {
                    crosshair.GetComponent<Image>().color = Color.red; // Set crosshair to red
                }
                else
                {
                    SelectionText.SetActive(true); // Make the text visible
                    crosshair.GetComponent<Image>().color = Color.white; // Reset crosshair to white
                }
            }
            else
            {
                onTarget = false;
                ResetUI(); // Reset the UI when no interactable object is found
            }
        }
        else
        {
            onTarget = false;
            ResetUI(); // Reset the UI if the raycast hits nothing
        }
    }

    // Helper method to reset the UI
    private void ResetUI()
    {
        crosshair.GetComponent<Image>().color = Color.white; // Reset crosshair to white
        SelectionText.SetActive(false); // Hide the text
    }
}

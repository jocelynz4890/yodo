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
    public GameObject player;

    public GameObject playerGun;
    public GameObject playerAxe;

    public BuildingPlacement _buildSettings;

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
        // handle item update

        if(_buildSettings.getHasToolbox())
        {
            playerGun.SetActive(false);
            playerAxe.SetActive(true);
        }
        else
        {
            playerGun.SetActive(true);
            playerAxe.SetActive(false);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
            onTarget = false;
            ResetUI(); // Reset the UI when no interactable object is found
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

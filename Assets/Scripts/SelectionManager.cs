using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public GameObject SelectionText;
    private GameObject crosshair;
    private TextMeshProUGUI interaction_text;

    private void Start()
    {
        interaction_text = SelectionText.GetComponent<TextMeshProUGUI>();
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            if (selectionTransform.GetComponent<InteractableObject>())
            {
                interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                if (interaction_text.text == "Zombie")
                {
                    crosshair.GetComponent<Renderer>().material.color = Color.red; // Change crosshair to red
                }
                else
                {
                    crosshair.GetComponent<Renderer>().material.color = Color.white; // Reset crosshair to white
                }

                SelectionText.SetActive(true);
            }
            else
            {
                crosshair.GetComponent<Renderer>().material.color = Color.white; // Reset crosshair to white
                SelectionText.SetActive(false);
            }
        }
        else
        {
            // Ensure the UI is hidden and crosshair is reset if no object is hit
            crosshair.GetComponent<Renderer>().material.color = Color.white;
            SelectionText.SetActive(false);
        }
    }
}

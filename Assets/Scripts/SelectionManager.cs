using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public GameObject SelectionText;
    TextMeshProUGUI interaction_text;

    private void Start()
    {
        interaction_text = SelectionText.GetComponent<TextMeshProUGUI>();
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
                SelectionText.SetActive(true);
            }
            else
            {
                SelectionText.SetActive(false);
            }
        }
        else
        {
            // Ensure the UI is hidden if no object is hit
            SelectionText.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameObject pineappleScript;

    private PlayerController playerController;
    private Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerController = transform.parent.parent.GetComponent<PlayerController>();
        playerHealth = transform.parent.parent.GetComponent<Health>();
        GetComponent<TextMeshProUGUI>().text = "Time left: " + pineappleScript.GetComponent<PineappleScript>().SecUntilPineappleFinish.ToString("F1");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
        else if (pineappleScript.GetComponent<PineappleScript>().SecUntilPineappleFinish <= 0f)
        {
            GetComponent<TextMeshProUGUI>().text = "You Win!";
            playerController.canMove = false;
            playerController.canLook = false;
            playerController.canFire = false;
            playerController.canInteract = false;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = "Time left: " + pineappleScript.GetComponent<PineappleScript>().SecUntilPineappleFinish.ToString("F1");
        }
    }
}


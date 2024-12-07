using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameObject pineappleScript;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.parent.GetComponent<PlayerController>();
        GetComponent<TextMeshProUGUI>().text = "Time left: " + pineappleScript.GetComponent<PineappleScript>().SecUntilPineappleFinish.ToString("F1");
    }

    // Update is called once per frame
    void Update()
    {
        if (pineappleScript.GetComponent<PineappleScript>().SecUntilPineappleFinish <= 0f)
        {
            GetComponent<TextMeshProUGUI>().text = "You Win!";
            player.canMove = false;
            player.canLook = false;
            player.canFire = false;
            player.canInteract = false;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = "Time left: " + pineappleScript.GetComponent<PineappleScript>().SecUntilPineappleFinish.ToString("F1");
        }
    }
}


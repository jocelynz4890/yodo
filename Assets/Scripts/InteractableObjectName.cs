using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class InteractableObjectName : MonoBehaviour
{
    // public string ItemName;

    // public bool player1InRange;
    // public bool player2InRange;

    // GameObject player1;
    // GameObject player2;

    // private bool player1Gun;
    // private bool player1Axe;

    // private bool player2Gun;
    // private bool player2Axe;

    // private ResourceManager player1ResourceManager;
    // private ResourceManager player2ResourceManager;

    // private SelectionManager selectionManager1;
    // private SelectionManager selectionManager2;

    // public int amount = 5;

    // private void Start()
    // {
    //     player1 = GameObject.Find("Player 1");
    //     player2 = GameObject.Find("Player 2");

    //     // Find each player's ResourceManager
    //     player1ResourceManager = player1.GetComponent<ResourceManager>();
    //     player2ResourceManager = player2.GetComponent<ResourceManager>();

    //     // player1Gun = player1.GetComponent<SelectionManager>().playerGun.activeSelf;
    //     selectionManager1 = player1.GetComponent<SelectionManager>();
    //     player1Gun = selectionManager1.playerGun.activeSelf;
    //     player1Axe = selectionManager1.playerAxe.activeSelf;
    //     // player1Axe = player1.GetComponent<SelectionManager>().playerAxe.activeSelf;

    //     // player2Gun = player2.GetComponent<SelectionManager>().playerGun.activeSelf;
    //     // player2Axe = player2.GetComponent<SelectionManager>().playerAxe.activeSelf;
    //     selectionManager2 = player2.GetComponent<SelectionManager>();
    //     player2Gun = selectionManager2.playerGun.activeSelf;
    //     player2Axe = selectionManager2.playerAxe.activeSelf;
    // }
 
    // public string GetItemName()
    // {
    //     return this.ItemName;
    // }

    // // ONLY DETECTS 1 PLAYER AT A TIME
    // private void OnTriggerEnter(Collider other)
    // {
    //    if(other.gameObject.name == "Player 1")
    //    {
    //         player1InRange = true;
    //    }
    //    if(other.gameObject.name == "Player 2")
    //    {
    //         player2InRange = true;
    //    }
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     if(other.gameObject.name == "Player 1")
    //     {
    //         player1InRange = false;
    //     }
    //     if(other.gameObject.name == "Player 2")
    //    {
    //         player2InRange = false;
    //    }
    // }
}

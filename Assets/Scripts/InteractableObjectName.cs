using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class InteractableObjectName : MonoBehaviour
{
    public string ItemName;

    public bool player1InRange;
    public bool player2InRange;

    GameObject player1;
    GameObject player2;

    private bool player1Gun;
    private bool player1Axe;

    private bool player2Gun;
    private bool player2Axe;

    private ResourceManager player1ResourceManager;
    private ResourceManager player2ResourceManager;

    private SelectionManager selectionManager1;
    private SelectionManager selectionManager2;

    public int amount = 5;

    private void Start()
    {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");

        // Find each player's ResourceManager
        player1ResourceManager = player1.GetComponent<ResourceManager>();
        player2ResourceManager = player2.GetComponent<ResourceManager>();

        // player1Gun = player1.GetComponent<SelectionManager>().playerGun.activeSelf;
        selectionManager1 = player1.GetComponent<SelectionManager>();
        player1Gun = selectionManager1.playerGun.activeSelf;
        player1Axe = selectionManager1.playerAxe.activeSelf;
        // player1Axe = player1.GetComponent<SelectionManager>().playerAxe.activeSelf;

        // player2Gun = player2.GetComponent<SelectionManager>().playerGun.activeSelf;
        // player2Axe = player2.GetComponent<SelectionManager>().playerAxe.activeSelf;
        selectionManager2 = player2.GetComponent<SelectionManager>();
        player2Gun = selectionManager2.playerGun.activeSelf;
        player2Axe = selectionManager2.playerAxe.activeSelf;
    }
 
    public string GetItemName()
    {
        return this.ItemName;
    }

    void Update()
    {
        // Update status of tools
        player1Gun = selectionManager1.playerGun.activeSelf;
        player1Axe = selectionManager1.playerAxe.activeSelf;
        player2Gun = selectionManager2.playerGun.activeSelf;
        player2Axe = selectionManager2.playerAxe.activeSelf;

        if(Input.GetKeyDown(KeyCode.P) && player1InRange && player1Axe)
        {
            Debug.Log("Item added to player 1 inventory");
            // add amount to player's inventory based on ItemName and player in range
            if(this.ItemName == "Tree") 
            {
                player1ResourceManager.AddResource("Tree", this.amount);
            }
            else if(this.ItemName == "Rock") 
            {
                player1ResourceManager.AddResource("Rock", this.amount);
            }
        }
        if(Input.GetKeyDown(KeyCode.P) && player2InRange && player2Axe)
        {
            Debug.Log("Item added to player 2 inventory");
            player2ResourceManager.AddResource("Tree", this.amount);
            // add amount to player's inventory based on ItemName and player in range
            if(this.ItemName == "Tree") // also check if player is in axe mode
            {
                player2ResourceManager.AddResource("Tree", this.amount);
            }
            else if(this.ItemName == "Rock") // also check if player is in axe mode
            {
                player2ResourceManager.AddResource("Rock", this.amount);
            }
        }
    }

    // ONLY DETECTS 1 PLAYER AT A TIME
    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.name == "Player 1")
       {
            Debug.Log("Player 1 in range");
            player1InRange = true;
       }
       if(other.gameObject.name == "Player 2")
       {
            Debug.Log("Player 2 in range");
            player2InRange = true;
       }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Player 1")
        {
            Debug.Log("Player 1 exiting range");
            player1InRange = false;
        }
        if(other.gameObject.name == "Player 2")
       {
            Debug.Log("Player 2 in range");
            player2InRange = false;
       }
    }
}

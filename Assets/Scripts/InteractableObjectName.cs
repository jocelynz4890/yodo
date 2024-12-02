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

    GameObject player1Gun;
    GameObject player1Axe;

    GameObject player2Gun;
    GameObject player2Axe;

    public int amount;

    private void Start()
    {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
    }
 
    public string GetItemName()
    {
        return ItemName;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && player1InRange)
        {
            Debug.Log("Item added to player 1 inventory");
            // add amount to player's inventory based on ItemName and player in range
            if(ItemName == "Tree") // also check if player is in axe mode
            {

            }
            else if(ItemName == "Rock") // also check if player is in axe mode
            {

            }
            else if(ItemName == "Axe")
            {
                if (player1 != null)
                {
                    player1Gun = player1.transform.Find("Gun").gameObject;
                    player1Axe = player1.transform.Find("axe").gameObject;
                }

                player1Gun.SetActive(false);
                player1Axe.SetActive(true);

                Debug.Log("Picking up axe");
            }
            else if(ItemName == "Gun")
            {
                if (player1 != null)
                {
                    player1Gun = player1.transform.Find("Gun").gameObject;
                    player1Axe = player1.transform.Find("axe").gameObject;
                }

                player1Gun.SetActive(true);
                player1Axe.SetActive(false);

                Debug.Log("Picking up gun");
            }
        }
        // copy for player 2
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

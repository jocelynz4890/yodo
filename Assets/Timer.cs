using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameObject pineappleScript;

    private PlayerController _playerController;
    private Health _playerHealth;
    
    void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _playerHealth = GetComponentInParent<Health>();
        GetComponent<TextMeshProUGUI>().text = "Time left: " + pineappleScript.GetComponent<PineappleScript>().SecUntilPineappleFinish.ToString("F1");
    }
    
    void Update()
    {
        if (_playerHealth.currentHealth <= 0)
        {
            GetComponent<TextMeshProUGUI>().text = "You are dead!";
        }
        else if (pineappleScript.GetComponent<PineappleScript>().SecUntilPineappleFinish <= 0f)
        {
            GetComponent<TextMeshProUGUI>().text = "You Win!";
            _playerController.canMove = false;
            _playerController.canLook = false;
            _playerController.canFire = false;
            _playerController.canInteract = false;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = "Time left: " + pineappleScript.GetComponent<PineappleScript>().SecUntilPineappleFinish.ToString("F1");
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Revive : MonoBehaviour
{
    public float reviveDuration = 5f;
    public float reviveRange = 2f;
    public GameObject otherPlayer;

    private bool otherIsDowned = false;
    private float reviveTimer = 0f;
    private PlayerController playerInput;
    private Health otherHealth;
    private TextMeshProUGUI text;
    private TextMeshProUGUI otherText;

    void Start () 
    {
        playerInput = GetComponentInParent<PlayerController>();
        otherHealth = otherPlayer.GetComponent<Health>();
        text = transform.Find("Canvas/Text").GetComponent<TextMeshProUGUI>();
        otherText = otherPlayer.transform.Find("Canvas/Text").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        otherIsDowned = otherHealth.currentHealth <= 0;

        if (otherIsDowned && playerInput.isInteract)
        {
            if (Vector3.Distance(transform.position, otherPlayer.transform.position) <= reviveRange)
            {
                reviveTimer += Time.deltaTime;
                text.text = "Reviving in " + (reviveDuration - reviveTimer).ToString("F1") + "s";
                otherText.text = "Revive in " + (reviveDuration - reviveTimer).ToString("F1") + "s";
                if (reviveTimer >= reviveDuration)
                {
                    RevivePlayer();
                }
            }
            else
            {
                text.text = "";
            }
        }
        else if (otherIsDowned)
        {
            reviveTimer = 0f;
            text.text = "";
            otherText.text = "Wait for revive.";
        }
    }

    public void RevivePlayer()
    {
        text.text = "";
        otherHealth.Heal(otherHealth.maxHealth / 2);
    }
}


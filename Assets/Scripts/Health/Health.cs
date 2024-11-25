using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Health : MonoBehaviour {
    public int maxHealth = 3;
    public int currentHealth = 3;

    private TextMeshProUGUI text;
    private TextMeshProUGUI healthText;
    private Slider playerHealthBar;
    private MeshRenderer zombieHealthBar;
    private float zombieMaxScale;

    void Start()
    {
        text = transform.Find("Canvas/Text")?.GetComponent<TextMeshProUGUI>();
        healthText = transform.Find("Canvas/Health/HealthText")?.GetComponent<TextMeshProUGUI>();
        playerHealthBar = transform.Find("Canvas/Health/HealthBar")?.GetComponent<Slider>();
        zombieHealthBar = transform.Find("HealthBar/Active")?.GetComponent<MeshRenderer>();

        if (playerHealthBar != null)
        {
            currentHealth = maxHealth;
            playerHealthBar.maxValue = maxHealth;
            playerHealthBar.value = currentHealth;
        }

        if (zombieHealthBar != null)
        {
            zombieMaxScale = zombieHealthBar.transform.localScale.x;
        }
    }

    public void Damage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            // Subtract damage from health
            currentHealth = Mathf.Max(0, currentHealth - damageAmount);
        }
        
        if (currentHealth == 0)
        {
            // Disable this object if it's a player. Otherwise, destroy it.
            if (gameObject.CompareTag("Player"))
            {
                gameObject.GetComponent<PlayerController>().enabled = false;
                gameObject.GetComponent<PlayerController>().ResetInputs();
                text.text = "Wait for revive.";
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void Heal(int healAmount)
    {
        if (currentHealth == 0)
        {
            // Revived so give back player controls
            if (gameObject.CompareTag("Player"))
            {
                gameObject.GetComponent<PlayerController>().enabled = true;
                text.text = "";
            }
        }

        if (currentHealth < maxHealth)
        {
            // Add health
            currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        }
    }

    void Update()
    {
        if (playerHealthBar != null)
        {
            // Update the health bar value based on the player's health
            playerHealthBar.value = currentHealth;
        }

        if (zombieHealthBar != null)
        {
            // Update the health bar value based on the zombie's health
            zombieHealthBar.transform.localScale = new Vector3(
                zombieMaxScale * (float)currentHealth / maxHealth,
                zombieHealthBar.transform.localScale.y,
                zombieHealthBar.transform.localScale.z
            );
            zombieHealthBar.transform.localPosition = new Vector3(
                zombieMaxScale * (1 - (float)currentHealth / maxHealth) / 2,
                zombieHealthBar.transform.localPosition.y,
                zombieHealthBar.transform.localPosition.z
            );
        }

        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
    }
}

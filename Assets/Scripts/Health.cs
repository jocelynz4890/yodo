using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    public int currentHealth = 3;

    public void Damage(int damageAmount)
    {
        // Subtract damage from health
        currentHealth -= damageAmount;

        // If health has fallen below zero, destroy this object
        if (currentHealth <= 0) 
        {
            Destroy(gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public float health = 100;
    public float recoverRate; // how much the health recovers each frame


    public void LoseHealth(float healthLost)
    {
        Debug.Log("Health lost: " + healthLost);
        if (health - healthLost <= 0)
        {
            health = 0;
            healthBar.fillAmount = health / 100;
            return;
        }
        // reduce health
        health -= healthLost;

        // update UI healthbar
        healthBar.fillAmount = health / 100;

        if (health <= 0)    
        {
            Debug.Log("you died of health complications");
        }
        
    }

    public float GetStamina()
    {
        return healthBar.fillAmount;
    }

    private void Update()
    {
        if (health < 100)
        {
            health+= recoverRate;
            healthBar.fillAmount = health / 100;
        }
    }


    public void GainHealth(int healthGained)
    {
        if (health + healthGained >= 100)
        {
            health = 100;
            healthBar.fillAmount = health / 100;
            return;
        }
        // increase health
        health += healthGained;

        // update UI healthbar
        healthBar.fillAmount = health / 100;

    }
}

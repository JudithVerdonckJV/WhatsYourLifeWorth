using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    [SerializeField] private HealthBar healthBar = null;
    private BasePlayer playerInfo = null;

    [SerializeField] private RoomIndexManagment roomInfo = null;

    private void Awake()
    {
        currentHealth = maxHealth;
        if (healthBar != null) healthBar.SetMaxHealth(maxHealth);
        if (healthBar != null) healthBar.SetHealth(currentHealth);

        playerInfo = GetComponent<BasePlayer>();
    }

    private void LateUpdate()
    {
        if (currentHealth <= 0)
        {
            if (gameObject.tag == "Player")
            {
                PlayerDeath();
            }
            else
            {
                if (roomInfo != null) roomInfo.ReduceEnemyCount();
                gameObject.SetActive(false);
            }
        }
    }

    public void Damage(int amount)
    {
        currentHealth -= amount;
        if (healthBar != null) healthBar.SetHealth(currentHealth);
    }
     
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetToMax()
    {
        currentHealth = maxHealth;
        if (healthBar != null) healthBar.SetHealth(currentHealth);
    }

    private void PlayerDeath()
    {
        if (playerInfo == null || healthBar == null) return;
        
        if (playerInfo.tries > 0)
        {
            playerInfo.PlayerRetry();            
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            healthBar.ChangeTries(playerInfo.tries);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            playerInfo.bowActive = false;
            SceneManager.LoadScene(6); // game over menu
        }
    }
}

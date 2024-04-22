using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
        Debug.Log("Health after damage change: " + currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
            Time.timeScale = 0;
            
        }
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        healthBar.setHealth(currentHealth);
        Debug.Log("Health after change: " + currentHealth);
    }
}

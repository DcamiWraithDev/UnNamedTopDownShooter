using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

        if (gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene("GameOver");
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}


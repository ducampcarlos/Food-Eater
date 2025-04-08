using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthBar;

    [Header("Explosion")]
    public GameObject explosionPrefab;

    [Header("Damage Over Time")]
    public float damagePerSecond = 1f;
    private bool isTakingPassiveDamage = true;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    void Update()
    {
        if (isTakingPassiveDamage && GameManager.Instance.gameStarted)
        {
            TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }


    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Explode();
        }
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        GameManager.Instance.GameOver(); // Necesita método en GameManager
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        healthBar.value = currentHealth;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthBar;

    [Header("Damage Over Time")]
    public float damagePerSecond = 1f;
    private bool isTakingPassiveDamage = true;

    [Header("Visual Feedback")]
    public ParticleSystem smokeGray;
    public ParticleSystem smokeBlack;
    public ParticleSystem fireEffect;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        // Asegurar que estén desactivados al inicio
        smokeGray?.Stop();
        smokeBlack?.Stop();
        fireEffect?.Stop();
    }

    void Update()
    {
        if (isTakingPassiveDamage && GameManager.Instance.gameStarted)
        {
            TakeDamage(damagePerSecond * Time.deltaTime);
        }

        UpdateVisualDamageFeedback();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Explode();
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        healthBar.value = currentHealth;
    }

    void UpdateVisualDamageFeedback()
    {
        float percent = currentHealth / maxHealth;

        if (percent < 0.15f)
        {
            ActivateEffect(smokeGray, true);
            ActivateEffect(smokeBlack, true);
            ActivateEffect(fireEffect, true);
        }
        else if (percent < 0.3f)
        {
            ActivateEffect(smokeGray, true);
            ActivateEffect(smokeBlack, true);
            ActivateEffect(fireEffect, false);
        }
        else if (percent < 0.6f)
        {
            ActivateEffect(smokeGray, true);
            ActivateEffect(smokeBlack, false);
            ActivateEffect(fireEffect, false);
        }
        else
        {
            ActivateEffect(smokeGray, false);
            ActivateEffect(smokeBlack, false);
            ActivateEffect(fireEffect, false);
        }
    }

    void ActivateEffect(ParticleSystem ps, bool active)
    {
        if (ps == null) return;

        if (active && !ps.isPlaying)
            ps.Play();
        else if (!active && ps.isPlaying)
            ps.Stop();
    }

    void Explode()
    {
        ExplosionManager.Instance.SpawnExplosion(transform.position);
        Destroy(gameObject);
        GameManager.Instance.GameOver();
    }
}

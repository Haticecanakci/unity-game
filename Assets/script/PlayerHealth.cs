using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI")]
    public Slider healthSlider;
    public TextMeshProUGUI healthText; // ← YENİ EKLENDİ

    void Start()
    {
        currentHealth = maxHealth;

        UpdateHealthUI(); // ← Başlangıçta UI güncelle
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;
        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHealthUI(); // ← HASAR ALINCA UI güncelle

        if (currentHealth <= 0)
            Die();
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if (healthText != null)
            healthText.text = currentHealth + " / " + maxHealth;
    }

    void Die()
    {
        var cc = GetComponent<CharacterController>();
        if (cc) cc.enabled = false;

        var pm = GetComponent<PlayerMovement>();
        if (pm) pm.enabled = false;

        var rw = GetComponent<RifleWeapon>();
        if (rw) rw.enabled = false;

        var goUI = FindObjectOfType<GameOverUI>();
        if (goUI != null)
            goUI.ShowGameOver();
    }
}

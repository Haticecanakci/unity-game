using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int _maximumHealth = 100;
    int _currentHealth = 0;

    PlayerStats playerStats;

    void Start()
    {
        _currentHealth = _maximumHealth;

        // PlayerStats sadece Player'da var, düşmanda yoksa null olur
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerStats = player.GetComponent<PlayerStats>();
    }

    public void Damage(int damageValue)
    {
        _currentHealth -= damageValue;

        if (_currentHealth <= 0)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        EnemySpawnManager.OnEnemyDeath();

        // 🔵 Öldürme sayacını artır
        KillCounter.AddKill();

        Destroy(gameObject);
    }

    public int CurrentHealth => _currentHealth;
}

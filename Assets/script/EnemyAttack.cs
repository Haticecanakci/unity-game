using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 5;
    public float attackDelay = 1f;

    float nextAttackTime = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (Time.time < nextAttackTime) return;

        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(damage);
            nextAttackTime = Time.time + attackDelay;
        }
    }
}

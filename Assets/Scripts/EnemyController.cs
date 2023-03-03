using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public BossData bossData;
    public OnBossKilledEvent onBossKilled;
    public OnDamageEvent onDamage;


    public void TakeDamage(int damage)
    {
        bossData.currentHealth -= damage;
        if (bossData.currentHealth > 0)
        {
            onDamage?.Invoke();
        }
        else
        {
            onBossKilled?.Invoke();
        }
    }

    private void onCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().onDamage?.Invoke();
        }
    }
}

// public delegate void OnBossKilledEvent();
// public delegate void OnDamageEvent();

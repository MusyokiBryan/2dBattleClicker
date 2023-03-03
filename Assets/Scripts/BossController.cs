using UnityEngine;

public class BossController : MonoBehaviour
{
    public BossData bossData;
    public OnBossKilledEvent onBossKilled;

    public void TakeDamage(int damage)
    {
        bossData.currentHealth -= damage;
        if (bossData.currentHealth <= 0)
        {
            onBossKilled?.Invoke();
        }
    }
}

public delegate void OnBossKilledEvent();

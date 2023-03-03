using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponData weaponData;
    public OnAttackEvent onAttack;

    private void OnTriggerEnter2D(Collider2D other)
    {
        BossController boss = other.GetComponent<BossController>();
        if (boss != null && boss.bossData.bossType == weaponData.bossType)
        {
            boss.TakeDamage(weaponData.damage);
            onAttack?.Invoke();
        }
    }
}

public delegate void OnAttackEvent();

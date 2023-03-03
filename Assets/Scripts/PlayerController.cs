using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public BossData bossData;
    public List<WeaponData> weapons;
    public int currentWeaponIndex = 0;
    public Text currentWeaponText;
    public OnDamageEvent onDamage;
    public float attackCooldown = 0.5f;

    private float lastAttackTime = 0f;

    private void Start()
    {
        // UpdateWeaponUI();
    }

    private void Update()
    {
        if (lastAttackTime + attackCooldown <= Time.time)
        {
            lastAttackTime = Time.time;
            Attack();
        }
    }

    public void SwitchWeapon()
    {
        currentWeaponIndex++;
        if (currentWeaponIndex >= weapons.Count)
        {
            currentWeaponIndex = 0;
        }
        UpdateWeaponUI();
    }

    public void UpdateWeaponUI()
    {
    currentWeaponText.text = "Current Weapon: " + weapons[currentWeaponIndex].weaponName;
    }

 public void Attack()
{
    if (bossData.currentHealth <= 0)
    {
        return;
    }
    WeaponData currentWeapon = weapons[currentWeaponIndex];
    currentWeapon.Use();
    if (currentWeapon.bossType == bossData.bossType)
    {
        bossData.currentHealth -= currentWeapon.damage;
        if (bossData.currentHealth <= 0)
        {
            onDamage?.Invoke();
        }
    }
    }
}

public delegate void OnDamageEvent();

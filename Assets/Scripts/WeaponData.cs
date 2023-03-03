using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Clicker Game/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int damage;
    public Sprite weaponSprite;
    public BossType bossType;
     public OnAttackEvent onAttack;

    public void Use()
    {
        onAttack?.Invoke();
    }
}

// public delegate void OnAttackEvent();



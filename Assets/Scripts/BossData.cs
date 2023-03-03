using UnityEngine;
using UnityEditor;

public enum BossType {
    Fire,
    Ice,
    Poison,
    Electric,
    // Add additional boss types as needed
}

[CreateAssetMenu(fileName = "BossData", menuName = "ScriptableObjects/BossData", order = 1)]
public class BossData : ScriptableObject
{
    new public string name;
    public int maxHealth;
    public int currentHealth;
    public BossType bossType;
}

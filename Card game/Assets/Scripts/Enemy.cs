using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private string enemyName = "Enemy";
    [SerializeField] private int maxHP = 100;
    [SerializeField] private int attack = 5;
    [SerializeField] private int armor = 0;

    public string EnemyName => enemyName;
    public int MaxHP => maxHP;
    public int HP { get; private set; }
    public int Attack => attack;
    public int Armor => armor;

    public event Action<Enemy> OnStatsChanged;
    public event Action<Enemy> OnDeath;

    private void Awake()
    {
        HP = Mathf.Clamp(HP, 0, maxHP);
        if (HP == 0) HP = maxHP;
        OnStatsChanged?.Invoke(this);
    }

    public void TakeDamage(int amount)
    {
        amount = Mathf.Max(0, amount);
        int final = Mathf.Max(0, amount - armor);

        HP = Mathf.Max(0, HP - final);
        OnStatsChanged?.Invoke(this);

        if (HP == 0)
            OnDeath?.Invoke(this);
    }

    public void Heal(int amount)
    {
        amount = Mathf.Max(0, amount);
        HP = Mathf.Min(maxHP, HP + amount);
        OnStatsChanged?.Invoke(this);
    }
}

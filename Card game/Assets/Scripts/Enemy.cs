using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private string enemyName = "Enemy";
    [SerializeField] private int maxHP = 100;
    [SerializeField] private int currentHP = 100;
    [SerializeField] private int attack = 5;
    [SerializeField] private int armor = 0;

    public string EnemyName => enemyName;
    public int MaxHP => maxHP;
    public int HP => currentHP;
    public int Attack => attack;
    public int Armor => armor;

    public event Action<Enemy> OnStatsChanged;
    public event Action<Enemy> OnDeath;

    private void Awake()
    {
        ClampHp();
        OnStatsChanged?.Invoke(this);
    }

    private void OnValidate()
    {
        // Editor/Inspector wijzigingen (ook tijdens Play Mode)
        ClampHp();

        if (Application.isPlaying)
            OnStatsChanged?.Invoke(this);
    }

    private void ClampHp()
    {
        if (maxHP < 1) maxHP = 1;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    public void TakeDamage(int amount)
    {
        amount = Mathf.Max(0, amount);
        int final = Mathf.Max(0, amount - armor);

        currentHP = Mathf.Max(0, currentHP - final);
        OnStatsChanged?.Invoke(this);

        if (currentHP == 0)
            OnDeath?.Invoke(this);
    }

    public void Heal(int amount)
    {
        amount = Mathf.Max(0, amount);
        currentHP = Mathf.Min(maxHP, currentHP + amount);
        OnStatsChanged?.Invoke(this);
    }
}
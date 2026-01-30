using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    [Header("UI")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text statsText;
    [SerializeField] private Image hpFill; // optional

    private void Awake()
    {
        if (enemy == null)
            enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        if (enemy == null) return;

        enemy.OnStatsChanged += HandleChanged;
        enemy.OnDeath += HandleDeath;

        Refresh();
    }

    private void OnDisable()
    {
        if (enemy == null) return;

        enemy.OnStatsChanged -= HandleChanged;
        enemy.OnDeath -= HandleDeath;
    }

    private void HandleChanged(Enemy e) => Refresh();

    private void HandleDeath(Enemy e)
    {
        Refresh();
        Debug.Log("Enemy died.");
    }

    private void Refresh()
    {
        if (enemy == null) return;

        if (nameText != null)
            nameText.text = enemy.EnemyName;

        if (statsText != null)
            statsText.text = $"HP: {enemy.HP}/{enemy.MaxHP}\nATK: {enemy.Attack}\nARM: {enemy.Armor}";

        if (hpFill != null)
            hpFill.fillAmount = enemy.MaxHP > 0 ? (float)enemy.HP / enemy.MaxHP : 0f;
    }
}
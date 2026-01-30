using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Multiple card selection
    private readonly HashSet<UICardSelectable> selectedCards = new();
    public IReadOnlyCollection<UICardSelectable> SelectedCards => selectedCards;

    // Active suit for the current selection
    public Sprite SelectedSuitSprite { get; private set; }


    public int GetSelectedDamageTotal()
    {
        int total = 0;
        foreach (var c in selectedCards)
            total += c != null ? c.Damage : 0;

        return total;
    }

    public void UseSelectedCardsOn(Enemy enemy)
    {
        if (enemy == null) return;
        int damage = GetSelectedDamageTotal();
        if (damage <= 0) return;

        enemy.TakeDamage(damage);
        ClearSelection();
        Debug.Log($"Damage {damage} done to {enemy.EnemyName}");
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ToggleSelectCard(UICardSelectable card)
    {
        if (card == null) return;

        Sprite suitSprite = card.SuitSprite;
        if (suitSprite == null)
        {
            Debug.LogWarning($"Card '{card.name}' has nomsuit");
            return;
        }

        // first selection
        if (SelectedSuitSprite == null)
            SelectedSuitSprite = suitSprite;

        // if diffrent suit is selected selection is thanos snapped
        if (SelectedSuitSprite != suitSprite)
        {
            ClearSelection();
            SelectedSuitSprite = suitSprite;
        }

        // toggle selection
        if (selectedCards.Contains(card))
        {
            selectedCards.Remove(card);
            card.SetSelected(false);
        }
        else
        {
            selectedCards.Add(card);
            card.SetSelected(true);
        }

        // nothing selected anymore
        if (selectedCards.Count == 0)
            SelectedSuitSprite = null;

        Debug.Log($"Selected Suit Sprite: {(SelectedSuitSprite != null ? SelectedSuitSprite.name : "None")} | Count: {selectedCards.Count}");
    }

    public void ClearSelection()
    {
        foreach (var c in selectedCards)
            c.SetSelected(false);

        selectedCards.Clear();
        SelectedSuitSprite = null;
    }
}
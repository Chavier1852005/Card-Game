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

    [SerializeField] private Handmanager hand;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        if (hand == null)
            hand = FindFirstObjectByType<Handmanager>();
    }

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

        // Snapshot voor ClearSelection, anders weet je niet meer welke kaarten je gebruikte
        var toReplace = new List<UICardSelectable>(selectedCards);

        enemy.TakeDamage(damage);
        ClearSelection();

        if (hand == null)
            hand = FindFirstObjectByType<Handmanager>();

        if (hand != null)
        {
            foreach (var s in toReplace)
            {
                if (s != null && s.View != null)
                    hand.ReplaceCard(s.View);
            }
        }

        Debug.Log($"Damage {damage} done to {enemy.EnemyName}");
    }

    public void ToggleSelectCard(UICardSelectable card)
    {
        if (card == null) return;

        Sprite suitSprite = card.SuitSprite;
        if (suitSprite == null)
        {
            Debug.LogWarning($"Card '{card.name}' has no suit sprite.");
            return;
        }

        // First selection starts a group
        if (SelectedSuitSprite == null)
            SelectedSuitSprite = suitSprite;

        // Different suit clicked clear and start new group
        if (SelectedSuitSprite != suitSprite)
        {
            ClearSelection();
            SelectedSuitSprite = suitSprite;
        }

        // Toggle selection
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

        // Nothing selected anymore
        if (selectedCards.Count == 0)
            SelectedSuitSprite = null;

        Debug.Log($"Selected Suit Sprite: {(SelectedSuitSprite != null ? SelectedSuitSprite.name : "None")} | Count: {selectedCards.Count}");
    }

    public void ClearSelection()
    {
        foreach (var c in selectedCards)
            if (c != null) c.SetSelected(false);

        selectedCards.Clear();
        SelectedSuitSprite = null;
    }
}
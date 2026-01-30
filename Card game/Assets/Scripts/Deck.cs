using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] public List<CardSO> deckOfCards;
    [SerializeField] public List<CardSO> discardPile;

    public bool TryDraw(out CardSO card)
    {
        if (deckOfCards == null) deckOfCards = new List<CardSO>();
        if (discardPile == null) discardPile = new List<CardSO>();

        if (deckOfCards.Count == 0)
            ReshuffleFromDiscard();

        if (deckOfCards.Count == 0)
        {
            card = null;
            return false;
        }

        int randomIndex = Random.Range(0, deckOfCards.Count);
        card = deckOfCards[randomIndex];
        deckOfCards.RemoveAt(randomIndex);
        return true;
    }

    public void Discard(CardSO card)
    {
        if (card == null) return;
        discardPile.Add(card);
    }

    private void ReshuffleFromDiscard()
    {
        if (discardPile.Count == 0) return;

        deckOfCards.AddRange(discardPile);
        discardPile.Clear();
    }
}



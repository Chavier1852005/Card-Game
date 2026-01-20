using System;
using System.Collections.Generic;

public sealed class Deck
{
    private readonly List<CardData> cards;
    private readonly System.Random rng;

    public int Count => cards.Count;

    public Deck(IEnumerable<CardData> initialCards, int? seed = null)
    {
        cards = new List<CardData>(initialCards);
        rng = seed.HasValue ? new System.Random(seed.Value) : new System.Random();
    }

    public void Shuffle()
    {
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }

    public bool TryDraw(out CardData card)
    {
        if (cards.Count == 0)
        {
            card = default;
            return false;
        }

        int lastIndex = cards.Count - 1;
        card = cards[lastIndex];
        cards.RemoveAt(lastIndex);
        return true;
    }
}

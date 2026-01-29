using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image suitIconImageTR;
    [SerializeField] private Image suitIconImageBL;
    [SerializeField] private TMP_Text rankText;
    private Deck deck;

    [Header("Art")]
    [SerializeField] private CardSO cardSO;

    public Sprite SuitSprite => cardSO != null ? cardSO.cardSuit : null;

    private void Start()
    {
        deck = FindFirstObjectByType<Deck>();
        RandomCardSelector();

        rankText.text = cardSO.cardNumber.ToString();
        suitIconImageTR.sprite = cardSO.cardSuit;
        suitIconImageBL.sprite = cardSO.cardSuit;
    }

    private void RandomCardSelector()
    {
        int randomIndex = Random.Range(0, deck.deckOfCards.Count);
        cardSO = deck.deckOfCards[randomIndex];
        deck.deckOfCards.RemoveAt(randomIndex);
    }
}
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

    public CardSO CardData => cardSO;
    public int CardNumber => cardSO != null ? cardSO.cardNumber : 0;
    public Sprite SuitSprite => cardSO != null ? cardSO.cardSuit : null;

    private void Start()
    {
        deck = FindFirstObjectByType<Deck>();
        
        if (cardSO == null && deck != null && deck.TryDraw(out var so))
        cardSO = so;
        
        RefreshUI();
    }

    public void SetCard(CardSO newCard)
    {
        cardSO = newCard;
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (cardSO == null) return;

        if (rankText != null) rankText.text = cardSO.cardNumber.ToString();
        if (suitIconImageTR != null) suitIconImageTR.sprite = cardSO.cardSuit;
        if (suitIconImageBL != null) suitIconImageBL.sprite = cardSO.cardSuit;
    }
}
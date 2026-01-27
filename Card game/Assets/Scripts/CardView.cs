using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    // public CardData Data { get; private set; }

    [Header("UI")]
    [SerializeField] private Image suitIconImageTR;
    [SerializeField] private Image suitIconImageBL;
    [SerializeField] private TMP_Text rankText;
    private Deck deck;
    private GameManager gameManager;


    [Header("Art")]
    [SerializeField] private CardSO cardSO;


    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        deck = FindFirstObjectByType<Deck>();
        RandomCardSelector();
        rankText.text = cardSO.cardNumber.ToString();
        suitIconImageTR.sprite = cardSO.cardSuit;
        suitIconImageBL.sprite = cardSO.cardSuit;
    }



    // private static string FormatRank(Rank rank)
    // {
    //     return rank switch
    //     {
    //         Rank.Ace => "A",
    //         Rank.Jack => "J",
    //         Rank.Queen => "Q",
    //         Rank.King => "K",
    //         _ => ((int)rank).ToString()
    //     };
    // }

    private void RandomCardSelector()
    {
        int randomIndex = Random.Range(0, deck.deckOfCards.Count);
        cardSO = deck.deckOfCards[randomIndex];
        deck.deckOfCards.RemoveAt(randomIndex);
    }

   public void SelectCard()
    {
        
    }
}

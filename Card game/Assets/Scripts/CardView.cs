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
    

    [Header("Art")]
    [SerializeField] private CardSO cardSO;


     private void Start() 
     {
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
}

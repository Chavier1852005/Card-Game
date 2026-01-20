using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    public CardData Data { get; private set; }

    [Header("UI")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image suitIconImage;
    [SerializeField] private TMP_Text rankText;

    [Header("Art")]
    [SerializeField] private Sprite cardBackground;
    [SerializeField] private SuitIconSet suitIcons;

    public void Init(CardData data)
    {
        Data = data;
        name = data.ToString();

        ApplyVisuals();
    }

    private void ApplyVisuals()
    {
        if (backgroundImage != null && cardBackground != null)
            backgroundImage.sprite = cardBackground;

        if (rankText != null)
            rankText.text = FormatRank(Data.Rank);

        if (suitIconImage != null && suitIcons != null)
            suitIconImage.sprite = suitIcons.GetIcon(Data.Suit);
    }

    private static string FormatRank(Rank rank)
    {
        return rank switch
        {
            Rank.Ace => "A",
            Rank.Jack => "J",
            Rank.Queen => "Q",
            Rank.King => "K",
            _ => ((int)rank).ToString()
        };
    }
}

using UnityEngine;

[CreateAssetMenu(menuName = "Card Game/Suit Icon Set", fileName = "SuitIconSet")]
public class SuitIconSet : ScriptableObject
{
    public Sprite hearts;
    public Sprite diamonds;
    public Sprite clubs;
    public Sprite spades;

    public Sprite GetIcon(Suit suit)
    {
        return suit switch
        {
            Suit.Hearts => hearts,
            Suit.Diamonds => diamonds,
            Suit.Clubs => clubs,
            Suit.Spades => spades,
            _ => null
        };
    }
}

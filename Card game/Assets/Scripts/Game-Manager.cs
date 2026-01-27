using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private Turn Current_Turn;
    public enum Turn
    {
        Player, Enemy
    }

    public UICardSelectable SelectedCard { get; private set; }
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

    void Start()
    {
        Current_Turn = Turn.Player;
    }

    public void SelectCard(UICardSelectable card)
    {
        if (card == null) return;

        if (SelectedCard == card)
        {
            SelectedCard.SetSelected(false);
            SelectedCard = null;
            return;
        }
        if (SelectedCard != null)
           SelectedCard.SetSelected(false);

        SelectedCard = card;
        SelectedCard.SetSelected(true);

        Debug.Log($"Selected: {card.name}");
    }
    void Update()
    {

    }

    public void SwitchTurn()
    {

        if (Current_Turn == Turn.Player) { Current_Turn = Turn.Enemy; } else { Current_Turn = Turn.Player; }

        switch (Current_Turn)
        {

            case Turn.Player:
                // iets gebeurt ja nee dat dus
                break;
            case Turn.Enemy:
                // en hier ook misschien ooit iets
                break;

        }

    }
}

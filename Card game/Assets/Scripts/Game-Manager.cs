using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    private Turn Current_Turn;
    public enum Turn
    {
        Player, Enemy
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Current_Turn = Turn.Player;
    }

    // Update is called once per frame
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

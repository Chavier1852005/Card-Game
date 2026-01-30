using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;

public class Handmanager : MonoBehaviour
{
    [SerializeField] private int maxHandSize;
    [SerializeField] private CardView cardPrefab;
    [SerializeField] private SplineContainer splinecontainer;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Deck deck;
    private readonly List<CardView> handCards = new();

    public int Count => handCards.Count;

    private void Awake()
    {
        if (deck == null)
            deck = FindFirstObjectByType<Deck>();
    }

    public void DrawCard()
    {
        if (handCards.Count >= maxHandSize) return;

        CardView view = Instantiate(cardPrefab, spawnPoint.position, spawnPoint.rotation);
        handCards.Add(view);
        UpdateCardPositions();
    }

    public void ReplaceCard(CardView view)
    {
        if (view == null) return;
        if (deck == null)
        {
            Debug.LogWarning("No deck found yet");
            return;
        }

        deck.Discard(view.CardData);

        if (deck.TryDraw(out var newCard))
            view.SetCard(newCard);
        else 
            Debug.Log("Deck is empty, cannot draw a new card");
        UpdateCardPositions();
    }

    private void UpdateCardPositions()
    {
        if(handCards.Count == 0) return;
        float cardSpacing = 1f / maxHandSize;
        float firstCardPosition = 0.5f - (handCards.Count - 1) * cardSpacing / 2;
        Spline spline = splinecontainer.Spline;
        for (int i = 0; i < handCards.Count; i++)
        {
          float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross (up, forward));
                        Transform t = handCards[i].transform;
                        t.DOMove(splinePosition, 0.25f);
                        t.DORotateQuaternion(rotation, 0.25f);
        }
    }


}
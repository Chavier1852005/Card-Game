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
    private readonly List<CardView> handCards = new();

    public int Count => handCards.Count;

    private void Update()
    {

        

    }
    public void DrawCard()
    {
        if (handCards.Count >= maxHandSize) return;

        CardView view = Instantiate(cardPrefab, spawnPoint.position, spawnPoint.rotation);
        handCards.Add(view);
        UpdateCadPositions();
    }

    public bool DrawCard(Deck deck)
    {
        if (handCards.Count >= maxHandSize) return false;
        if (deck == null) return false;
        if (!deck.TryDraw(out CardData data)) return false;

        CardView view = Instantiate(cardPrefab, spawnPoint.position, spawnPoint.rotation);
        view.Init(data);
        handCards.Add(view);
        UpdateCadPositions();
        return true;
    }

    private void UpdateCadPositions()
    {
        if(handCards.Count == 0) return;
        float cardSpacing = 1f / maxHandSize;
        float firstCardPosition = 0.5f - (handCards.Count - 1) * cardSpacing / 2;
        Spline spline = splinecontainer.Spline;
        for (int i = 0; i < handCards.Count; i++)
        {
          float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 foward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross (up, foward));
                        Transform t = handCards[i].transform;
                        t.DOMove(splinePosition, 0.25f);
                        t.DORotateQuaternion(rotation, 0.25f);
        }
    }
}
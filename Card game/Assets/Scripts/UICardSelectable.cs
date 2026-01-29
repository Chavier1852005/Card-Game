using UnityEngine;
using UnityEngine.EventSystems;

public class UICardSelectable : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CardView cardView;

    public bool IsSelected { get; private set; }

    private RectTransform rt;
    private Vector2 baseAnchoredPos;

    public Sprite SuitSprite => cardView != null ? cardView.SuitSprite : null;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        baseAnchoredPos = rt.anchoredPosition;

        if (cardView == null)
            cardView = GetComponent<CardView>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.ToggleSelectCard(this);
    }

    public void SetSelected(bool selected)
    {
        IsSelected = selected;

        // kaartje gaat omhoog bij het selecteren
        rt.anchoredPosition = baseAnchoredPos + (IsSelected ? new Vector2(0, 30) : Vector2.zero);

        Debug.Log($"{name} IsSelected = {IsSelected}");
    }
}
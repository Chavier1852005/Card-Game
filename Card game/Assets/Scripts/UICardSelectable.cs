using UnityEngine;
using UnityEngine.EventSystems;

public class UICardSelectable : MonoBehaviour, IPointerClickHandler
{
    public bool IsSelected { get; private set; }
    private RectTransform rt;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.SelectCard(this);
    }

    public void SetSelected(bool selected)
    {
        IsSelected = selected;

        Debug.Log($"{name} IsSelected = {IsSelected}");

        if (IsSelected)
        {
            rt.anchoredPosition += new Vector2(0, 30);
        }
        else
        {
            rt.anchoredPosition -= new Vector2(0, 30);
        }
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rt = GetComponent <RectTransform>();
    }

    // // Update is called once per frame
    // void Update()
    // {

    // }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyTarget : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Enemy enemy;

    private void Awake()
    {
        if (enemy == null)
            enemy = GetComponent<Enemy>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance == null || enemy == null) return;
        GameManager.Instance.UseSelectedCardsOn(enemy);
    }
}

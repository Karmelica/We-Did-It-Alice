using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableClothingPiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private Transform defaultParent;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Canvas canvas;

    public string requiredSlotTag;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        defaultParent = transform.parent; // zapamiętaj domyślnego parenta
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(canvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        GameObject target = eventData.pointerEnter;
        if (target != null && target.GetComponent<LayoutGroup>() != null)
        {
            if (target.transform.childCount == 0)
            {
                transform.SetParent(target.transform, false);
                if (target.CompareTag(requiredSlotTag))
                {
                    //Debug.Log($"{gameObject.name} poprawnie umieszczony w slocie {requiredSlotTag}");
                }
                else
                {
                    //Debug.LogWarning($"{gameObject.name} NIE jest w odpowiednim slocie! Oczekiwano: {requiredSlotTag}, znaleziono: {target.tag}");
                }
            }
            else
            {
                transform.SetParent(originalParent, false);
            }
        }
        else
        {
            // Odkładanie poza slotem: wracamy do domyślnego parenta (np. lista ubrań)
            transform.SetParent(defaultParent, false);
        }
    }
}

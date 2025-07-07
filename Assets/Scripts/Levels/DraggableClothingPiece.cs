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

    // Referencja do okna dialogowego (przypisz w Inspectorze lub znajdź w kodzie)
    public LvlTwoDialogueBox dialogueBox;
    public DialogueLine[] correctSlotLines;
    public DialogueLine[] wrongSlotLines;
    public DialogueLine[] perfectSlotLines;

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
        AudioManager.Instance.PlaySoundOneShot(FMODEvents.Instance.clothes, transform.position);
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
                    if (CompareTag("PerfectMatch"))
                    {
                        if (dialogueBox != null && perfectSlotLines != null && perfectSlotLines.Length > 0)
                        {
                            dialogueBox.gameObject.SetActive(true);
                            dialogueBox.SetLinesAndShow(perfectSlotLines);
                        }
                    }
                    // Wyświetl dialog o poprawnym umieszczeniu
                    else if (dialogueBox != null && correctSlotLines != null && correctSlotLines.Length > 0)
                    {
                        dialogueBox.gameObject.SetActive(true);
                        dialogueBox.SetLinesAndShow(correctSlotLines);
                    }
                }
                else
                {
                    // Wyświetl dialog o błędnym slocie
                    if (dialogueBox != null && wrongSlotLines != null && wrongSlotLines.Length > 0)
                    {
                        dialogueBox.gameObject.SetActive(true);
                        dialogueBox.SetLinesAndShow(wrongSlotLines);
                    }
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

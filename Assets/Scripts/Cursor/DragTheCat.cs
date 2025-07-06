using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DragTheCat : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private Coroutine resetCoroutine;
    private bool isOverTarget;
    private Vector2 offset;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    private void Start()
    {
        AudioManager.Instance.PlayLevelSelectAmbient();
    }

    private void OnDisable()
    {
        AudioManager.Instance.menuAmbientInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
            resetCoroutine = null;
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out offset
        );
        offset.y = rectTransform.rect.height / 2;
        isOverTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out var localPoint
        );

        float horizontalOffset = localPoint.x - rectTransform.anchoredPosition.x;
        float swingAngle = Mathf.Clamp(horizontalOffset * 2f, -50f, 50f);
        rectTransform.rotation = Quaternion.Euler(0, 0, -swingAngle);

        rectTransform.anchoredPosition = localPoint - new Vector2(0, offset.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DropTarget foundTarget = null;
        var pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        foreach (var result in raycastResults)
        {
            var dropTarget = result.gameObject.GetComponent<DropTarget>();
            if (dropTarget != null)
            {
                foundTarget = dropTarget;
                break;
            }
        }

        if (foundTarget != null)
        {
            SetOverTarget(true);
            MenuManager.Instance.gameProgression = foundTarget.levelNumber;
            SceneManager.LoadScene(MenuManager.Instance.gameProgression + 1);
        }
        else
        {
            resetCoroutine = StartCoroutine(ResetPositionAndRotation());
        }
    }

    private IEnumerator ResetPositionAndRotation()
    {
        Vector2 startPosition = rectTransform.anchoredPosition;
        Quaternion startRotation = rectTransform.rotation;
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, originalPosition, elapsed / duration);
            rectTransform.rotation = Quaternion.Lerp(startRotation, Quaternion.identity, elapsed / duration);
            yield return null;
        }

        rectTransform.anchoredPosition = originalPosition;
        rectTransform.rotation = Quaternion.identity;
    }

    public void SetOverTarget(bool isOver)
    {
        isOverTarget = isOver;
    }
}
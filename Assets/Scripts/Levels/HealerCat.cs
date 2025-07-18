using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealerCat : MonoBehaviour
{
    private Animator animator;
    public GraphicRaycaster graphicRaycaster;
    public float moveSpeed = 800f;
    private Vector3? targetPosition = null;
    private RectTransform rectTransform;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        bool isWalking = false;

        if (Input.GetMouseButtonDown(0))
        {
            targetPosition = Input.mousePosition;
        }

        if (targetPosition.HasValue)
        {
            rectTransform.position = Vector3.MoveTowards(rectTransform.position, targetPosition.Value, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(rectTransform.position, targetPosition.Value) < 1f)
            {
                rectTransform.position = targetPosition.Value;
                targetPosition = null;
            }
            else
            {
                isWalking = true;
            }
        }

        animator.SetBool("IsWalking", isWalking);

        // Pobierz wszystkie serca z LvlThreeManager
        var allHearts = LvlThreeManager.Instance != null ? LvlThreeManager.Instance.heartHealth : null;
        // Znajdź serce pod kotem
        var heartImg = GetHeartHealerImageUnderCat();
        HeartHealth heartUnderCat = null;
        if (heartImg != null)
        {
            heartUnderCat = heartImg.GetComponent<HeartHealth>();
        }
        // Ustaw healing/draining odpowiednio
        if (allHearts != null)
        {
            foreach (var heart in allHearts)
            {
                if (heart == heartUnderCat)
                {
                    heart.healing = true;
                    heart.draining = false;
                }
                else
                {
                    heart.healing = false;
                    heart.draining = true;
                }
            }
        }

        if (heartUnderCat != null && !isWalking)
        {
            animator.SetBool("IsHealing", true);
        }
        else
        {
            animator.SetBool("IsHealing", false);
        }
    }

    // Funkcja wykrywająca Image pod kotem, który ma komponent HeartHealer
    public Image GetHeartHealerImageUnderCat()
    {
        PointerEventData ped = new PointerEventData(EventSystem.current);
        ped.position = RectTransformUtility.WorldToScreenPoint(null, rectTransform.position);
        var results = new List<RaycastResult>();
        if (graphicRaycaster != null)
        {
            graphicRaycaster.Raycast(ped, results);
            foreach (var r in results)
            {
                var img = r.gameObject.GetComponent<Image>();
                if (img != null && img.gameObject != gameObject && img.GetComponent<HeartHealth>() != null)
                    return img;
            }
        }
        return null;
    }
}

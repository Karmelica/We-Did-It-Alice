using UnityEngine;
using UnityEngine.UI;

public class MouseFollower : MonoBehaviour
{
    private Animator animator;
    public float stopDistance = 30f; // dystans w pikselach, przy którym kot się zatrzymuje
    public float moveSpeed = 500f; // prędkość w pikselach na sekundę
    public Canvas canvas; // przypisz Canvas w Inspectorze

    private RectTransform rectTransform;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
        if (canvas == null)
            canvas = GetComponentInParent<Canvas>();
    }

    void Update()
    {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out mousePos);

        Vector2 direction = mousePos - rectTransform.anchoredPosition;
        float distance = direction.magnitude;

        // Obracanie tylko w osi Y (flip w lewo/prawo) z negatywną skalą, jeśli kot domyślnie patrzy w drugą stronę
        if (direction.x != 0)
        {
            Vector3 scale = rectTransform.localScale;
            scale.x = -Mathf.Sign(direction.x) * Mathf.Abs(scale.x);
            rectTransform.localScale = scale;
        }

        bool isWalking = false;

        if (distance > stopDistance)
        {
            // Podąża za kursorem
            Vector2 moveDir = direction.normalized;
            rectTransform.anchoredPosition += moveDir * (moveSpeed * Time.deltaTime);
            isWalking = true;
        }
        else if (distance < stopDistance * 0.8f)
        {
            // Odsuwa się od kursora, jeśli jest za blisko
            Vector2 moveDir = -direction.normalized;
            rectTransform.anchoredPosition += moveDir * (moveSpeed * Time.deltaTime);
            isWalking = true;
        }

        animator.SetBool("IsWalking", isWalking);
    }
}

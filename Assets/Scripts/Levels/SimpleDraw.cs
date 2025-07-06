using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleDraw : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Image progressBar;
    
    public Color drawColor = Color.black;
    public int brushSize = 4;

    private Texture2D tex;
    private bool isDrawing = false;
    private RectTransform rectTransform;
    private Vector2? lastDrawPos = null;
    private float drawnDistance = 0f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        RawImage img = GetComponent<RawImage>();
        tex = new Texture2D(512, 512, TextureFormat.RGBA32, false);
        tex.filterMode = FilterMode.Point;
        img.texture = tex;
        ClearTexture();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDrawing = true;
        lastDrawPos = null;
        DrawAt(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDrawing = false;
        lastDrawPos = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDrawing)
            DrawAt(eventData);
    }

    void DrawAt(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            float x = (localPoint.x + rectTransform.rect.width / 2) / rectTransform.rect.width * tex.width;
            float y = (localPoint.y + rectTransform.rect.height / 2) / rectTransform.rect.height * tex.height;

            Vector2 currentDrawPos = new Vector2(x, y);
            if (lastDrawPos.HasValue)
            {
                drawnDistance += Vector2.Distance(lastDrawPos.Value, currentDrawPos);
                float percent = Mathf.Clamp01(drawnDistance / 5200f) * 100f;
                progressBar.fillAmount = percent / 100f;
                if (percent >= 100f)
                {
                    LvlOneManager.Instance.canContinue = true;
                }
            }
            lastDrawPos = currentDrawPos;

            for (int i = -brushSize; i <= brushSize; i++)
            {
                for (int j = -brushSize; j <= brushSize; j++)
                {
                    int px = Mathf.Clamp((int)x + i, 0, tex.width - 1);
                    int py = Mathf.Clamp((int)y + j, 0, tex.height - 1);
                    tex.SetPixel(px, py, drawColor);
                }
            }
            tex.Apply();
        }
    }

    public void ClearAll()
    {
        ClearTexture();
        drawnDistance = 0f;
        progressBar.fillAmount = 0f;
    }

    private void ClearTexture()
    {
        Color32[] clearColors = new Color32[tex.width * tex.height];
        for (int i = 0; i < clearColors.Length; i++) clearColors[i] = new Color32(0, 0, 0, 0);
        tex.SetPixels32(clearColors);
        tex.Apply();
    }
}
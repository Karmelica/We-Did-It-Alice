using UnityEngine;
using UnityEngine.UI;

public class PutClothes : MonoBehaviour
{
    public GameObject headCorrectPiece;
    public GameObject bodyCorrectPiece;
    public GameObject legCorrectPiece;
    public GameObject footCorrectPiece;

    public GameObject headSlot;
    public GameObject bodySlot;
    public GameObject legSlot;
    public GameObject footSlot;

    public float progress = 0f;
    public Image progressBar;

    void Update()
    {
        float newProgress = 0f;
        if (IsPieceInSlot(headCorrectPiece, headSlot)) newProgress += 0.25f;
        if (IsPieceInSlot(bodyCorrectPiece, bodySlot)) newProgress += 0.25f;
        if (IsPieceInSlot(legCorrectPiece, legSlot)) newProgress += 0.25f;
        if (IsPieceInSlot(footCorrectPiece, footSlot)) newProgress += 0.25f;

        if (Mathf.Abs(progress - newProgress) > 0.001f)
        {
            progress = newProgress;
            Debug.Log($"Progress: {progress * 100}%");
            
        }
    
        progressBar.fillAmount = progress;
        
        if (!(progress >= 1f)) return;
        Debug.Log("Wszystkie elementy poprawnie umieszczone!");
        LvlTwoManager.Instance.canContinue = true; // Przykład dla LvlTwoManager
    }

    bool IsPieceInSlot(GameObject piece, GameObject slot)
    {
        if (piece == null || slot == null) return false;
        // Sprawdź, czy element jest dzieckiem slotu (LayoutGroup)
        return piece.transform.parent == slot.transform;
    }
}
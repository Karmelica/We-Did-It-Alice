using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlFourManager : MonoBehaviour
{
    public RectTransform[] trailPoints;
    
    private bool canContinue;
    private bool endSequenceStarted;
    public string[] completedDialogueLines;
    public LvlFourDialogueBox dialogueBox;
    static public LvlFourManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    void Update()
    {
        
        if (endSequenceStarted) return;
        if (!canContinue) return;
        endSequenceStarted = true;
        StartCoroutine(HandleWin());
    }
    
    private IEnumerator HandleWin()
    {
        dialogueBox.gameObject.SetActive(true);
        dialogueBox.StartDialogue(completedDialogueLines);
        // Czekaj aż okno dialogowe się zamknie
        while (!dialogueBox.completedDialogue)
            yield return null;
        // Przejdź do sceny o indeksie 1
        SceneManager.LoadScene(1);
    }
}

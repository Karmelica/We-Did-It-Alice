using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlOneManager : MonoBehaviour
{
    static public LvlOneManager Instance;
    public bool canContinue = false;
    public LvlOneDialogueBox dialogueBox;
    public string[] completedDialogueLines;
    private bool endSequenceStarted;
    
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

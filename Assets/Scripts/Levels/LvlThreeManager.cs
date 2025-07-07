using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlThreeManager : MonoBehaviour
{
    static public LvlThreeManager Instance;
    public bool canContinue = false;
    public bool failed = false;
    
    public LvlThreeDialogueBox dialogueBox;
    public string[] failedDialogueLines;
    public string[] completedDialogueLines;
    
    public List<HeartHealth> heartHealth;
    
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

    void StartHeartDraining()
    {
        foreach (var heart in heartHealth)
        {
            heart.draining = true;
        }
    }

    private void Start()
    {
        StartHeartDraining();
    }

    private bool endSequenceStarted = false;

    void Update()
    {
        if (endSequenceStarted) return;

        if (failed || canContinue) 
        {
            endSequenceStarted = true;
            if (failed)
            {
                StartCoroutine(HandleFailed());
            }
            else if (canContinue)
            {
                StartCoroutine(HandleWin());
            }
            return;
        }

        // Najpierw sprawdź, czy któreś serce nie ma mniej niż 1f
        foreach (var heart in heartHealth)
        {
            if (heart.health < 1f)
            {
                failed = true;
                canContinue = false;
                return;
            }
        }

        // Następnie sprawdź, czy wszystkie serca mają >= 75f
        bool allAbove75 = true;
        foreach (var heart in heartHealth)
        {
            if (heart.health < 75f)
            {
                allAbove75 = false;
                break;
            }
        }
        canContinue = allAbove75;
    }

    private IEnumerator HandleFailed()
    {
        dialogueBox.gameObject.SetActive(true);
        dialogueBox.SetLinesAndShow(failedDialogueLines);
        // Czekaj aż okno dialogowe się zamknie
        while (!dialogueBox.completedDialogue)
            yield return null;
        // Restartuj scenę
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public abstract class DialogueManager : MonoBehaviour
{
    public bool completedDialogue;
    
    public Image portraitImage;
    
    public float typingSpeed = 0.05f;
    
    public TextMeshProUGUI dialogueText;
    
    public DialogueLine[] dialogueLines;
    
    private int currentLineIndex = 0;   

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StopAllCoroutines();
            completedDialogue = true;
            gameObject.SetActive(false); // Ukryj okienko dialogowe
        }
    }

    public void StartDialogue(DialogueLine[] thisDialogueLines)
    {
        StopAllCoroutines();
        completedDialogue = false;
        if (dialogueLines.Length > 0)
        {
            StartCoroutine(PlayDialogue(thisDialogueLines));
        }
    }

    IEnumerator PlayDialogue(DialogueLine[] localDialogueLines)
    {
        for (currentLineIndex = 0; currentLineIndex < localDialogueLines.Length; currentLineIndex++)
        {
            if(portraitImage)
            {
                portraitImage.sprite = localDialogueLines[currentLineIndex].portrait;
            }  
            yield return StartCoroutine(TypeLine(localDialogueLines[currentLineIndex].text));
            yield return new WaitForSeconds(2f); // przerwa między linijkami
        }
        // Wyłącz okienko dialogowe po zakończeniu
        completedDialogue = true;
        gameObject.SetActive(false);
    }
    
    IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";
        foreach (char letter in line)
        {
            dialogueText.text += letter;
            if (!char.IsWhiteSpace(letter))
            {
                AudioManager.Instance.PlaySoundOneShot(FMODEvents.Instance.dialogue, transform.position);
            }
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    
    public void SetLinesAndShow(DialogueLine[] lines)
    {
        StopAllCoroutines();
        if (lines != null && lines.Length > 0)
        {
            // Wybierz losową linijkę
            dialogueLines = new[] { lines[Random.Range(0, lines.Length)] };
        }
        else
        {
            dialogueLines = Array.Empty<DialogueLine>();
        }
        StartDialogue(dialogueLines);
    }
}

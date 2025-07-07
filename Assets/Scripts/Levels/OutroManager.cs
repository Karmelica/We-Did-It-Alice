using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutroManager : MonoBehaviour
{
    static public OutroManager Instance;
    public OutroDialogueBox dialogueBox;
    private bool endSequenceStarted;
    
    private void Start()
    {
        StartCoroutine(HandleWin());
    }

    private IEnumerator HandleWin()
    {
        while (!dialogueBox.completedDialogue)
            yield return null;
        yield return new WaitForSeconds(5f);
        Application.Quit();
    }
}


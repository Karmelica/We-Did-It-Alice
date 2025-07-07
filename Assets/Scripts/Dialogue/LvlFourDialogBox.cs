using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlFourDialogueBox : DialogueManager
{
    void Start()
    {
        StartDialogue(dialogueLines);
    }
}

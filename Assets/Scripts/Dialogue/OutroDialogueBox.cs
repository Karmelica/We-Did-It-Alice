using UnityEngine;

public class OutroDialogueBox : DialogueManager
{
    void Awake()
    {
        StartDialogue(dialogueLines);
    }
}

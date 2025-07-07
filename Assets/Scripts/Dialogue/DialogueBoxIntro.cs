using UnityEngine;

public class DialogueBoxIntro : DialogueManager
{
    void Start()
    {
        if (MenuManager.Instance.introCompleted) return;
        StartDialogue(dialogueLines);
        MenuManager.Instance.introCompleted = true;
    }
}

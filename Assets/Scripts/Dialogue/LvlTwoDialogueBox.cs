using System;
using Random = UnityEngine.Random;

public class LvlTwoDialogueBox : DialogueManager
{
    void Start()
    {
        StartDialogue(dialogueLines);
    }
}

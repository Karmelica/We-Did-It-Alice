using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [TextArea(1, 5)]
    public string text;
    
    public Sprite portrait; 
}

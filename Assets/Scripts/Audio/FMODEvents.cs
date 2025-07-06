using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents Instance;
    
    [Header("UI")]
    public EventReference uiButtonClick;
    public EventReference starHover;
    public EventReference starClick;

    [Header("Ambient")]
    public EventReference menuAmbient;
    public EventReference levelSelectAmbient;
    
    [Header("SFX")]
    public EventReference catPurr;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
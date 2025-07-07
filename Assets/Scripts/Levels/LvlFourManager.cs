using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LvlFourManager : MonoBehaviour
{
    public RectTransform[] trailPoints;
    public GameObject glooberSpawner;

    public Image progressBar;
    private bool gameStarted;
    
    private bool canContinue;
    private bool endSequenceStarted;
    public DialogueLine[] completedDialogueLines;
    public DialogueLine[] goodLines;
    public DialogueLine[] badLines;
    public LvlFourDialogueBox dialogueBox;
    static public LvlFourManager Instance;
    
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

        StartCoroutine(AtStart());
    }
    

    void Update()
    {
        if (endSequenceStarted) return;
        
        if (gameStarted && progressBar.fillAmount < 1f)
        {
            progressBar.fillAmount += Time.deltaTime * 0.02f;
        }
        if(progressBar.fillAmount >= 1f && !canContinue)
        {
            canContinue = true;
        }
        
        
        
        if (!canContinue) return;
        endSequenceStarted = true;
        StartCoroutine(HandleWin());
    }
    
    private IEnumerator AtStart()
    {
        while (!dialogueBox.completedDialogue)
            yield return null;
        glooberSpawner.SetActive(true);
        gameStarted = true;
    }
    
    private IEnumerator HandleWin()
    {
        dialogueBox.gameObject.SetActive(true);
        dialogueBox.StartDialogue(completedDialogueLines);
        while (!dialogueBox.completedDialogue)
            yield return null;
        SceneManager.LoadScene(6);
        AudioManager.Instance.levelAmbient.setParameterByName("Level", 4);
    }
}

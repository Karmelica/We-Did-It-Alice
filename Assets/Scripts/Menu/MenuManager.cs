using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("Menu Panels")] [SerializeField]
    private Animator menuCanvasAnimator;

    public int gameProgression;
    public bool introCompleted;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void EnterBrain()
    {
        menuCanvasAnimator.SetTrigger("ClickedHead");
    }
    
    public void Begin()
    {
        menuCanvasAnimator.SetTrigger("ClickedBegin");
    }
    
    public void Options()
    {
        menuCanvasAnimator.SetTrigger("ClickedCredits");
    }
    
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}

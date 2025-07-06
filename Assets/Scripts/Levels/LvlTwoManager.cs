using UnityEngine;
using UnityEngine.SceneManagement;


public class LvlTwoManager : MonoBehaviour
{
    static public LvlTwoManager Instance;
    public bool canContinue = false;
    
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
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canContinue)
        {
            SceneManager.LoadScene(1);
        }
    }
}

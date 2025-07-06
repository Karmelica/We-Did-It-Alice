using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlOneManager : MonoBehaviour
{
    static public LvlOneManager Instance;
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

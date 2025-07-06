using UnityEngine;

public class AnimEventsSelector : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        MenuManager.Instance.LoadScene(sceneIndex);
    }
}

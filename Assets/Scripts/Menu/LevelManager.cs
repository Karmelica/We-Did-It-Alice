using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject dialogueBox;
    
    [Header("Level Progression")]
    [SerializeField] private Image levelStatus;
    [SerializeField] private List<Image> enabledLevels = new();
    [SerializeField] private List<GameObject> unlockedGlitter = new();
    [SerializeField] private List<Sprite> progressionSprites = new();
    

    private void Start()
    {
        ChangeLevelProgress();
        dialogueBox.SetActive(!MenuManager.Instance.introCompleted);
    }

    public void ChangeLevelProgress()
    {
        levelStatus.sprite = progressionSprites[MenuManager.Instance.gameProgression];
        for (int i = 0; i < 4; i++)
        {
            enabledLevels[i].gameObject.SetActive(i == MenuManager.Instance.gameProgression);
        }
        for (int i = 0; i < 3; i++)
        {
            unlockedGlitter[i].SetActive(i < MenuManager.Instance.gameProgression);
        }
    }
}

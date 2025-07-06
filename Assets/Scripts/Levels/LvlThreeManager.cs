using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlThreeManager : MonoBehaviour
{
    static public LvlThreeManager Instance;
    public bool canContinue = false;
    
    public List<HeartHealth> heartHealth;
    
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

    void StartHeartDraining()
    {
        foreach (var heart in heartHealth)
        {
            heart.draining = true;
        }
    }

    private void Start()
    {
        StartHeartDraining();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canContinue)
        {
            SceneManager.LoadScene(1);
        }
        
        foreach (var heart in heartHealth)
        {
            if (heart.health < 75f)
            {
                return;
            }
            canContinue = true;
        }
        
        foreach (var heart in heartHealth)
        {
            if (heart.health < 1f)
            {
                Debug.Log("Game Over");
                return;
            }
        }
    }
}

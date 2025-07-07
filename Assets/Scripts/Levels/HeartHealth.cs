using System;
using UnityEngine;
using UnityEngine.Serialization;

public class HeartHealth : MonoBehaviour
{
    private Animator _animator;
    public float health = 100f;
    public bool draining;
    public bool healing;
    public LvlThreeDialogueBox dialogueBox;
    public LvlThreeManager lvlThreeManager;

    private float cooldownTimer = 0f;
    private const float cooldownDuration = 5f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void DrainLife()
    {
        if (cooldownTimer > 0f) return; // cooldown aktywny, nie trac zdrowia
        if (health > 0f)
            health -= Time.deltaTime * 2f;
    }

    private void RegenerateLife()
    {
        if (health < 100f)
            health += Time.deltaTime * 20f;
        if (health >= 100f)
        {
            health = 100f;
            cooldownTimer = cooldownDuration; // uruchom cooldown
        }
    }

    private void Update()
    {
        if(!dialogueBox.completedDialogue) return;
        if(lvlThreeManager.canContinue || lvlThreeManager.failed) return;
        
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0f) cooldownTimer = 0f;
        }

        if (draining)
        {
            DrainLife();
        }
        if (healing)
        {
            RegenerateLife();
        }
        _animator.SetFloat("Health", health);
    }
}

using System;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialCatzu : MonoBehaviour
{
    private Animator animator;
    private Collider2D coli2D;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gloober"))
        {
            AudioManager.Instance.PlaySoundOneShot(FMODEvents.Instance.slap, transform.position);
            other.GetComponent<Gloober>().KillYourself();
        }
    }

    public void EnableCollider()
    {
        coli2D.enabled = true;
    }
    
    public void DisableCollider()
    {
        coli2D.enabled = false;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        coli2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
    }
}

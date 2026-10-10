
using System;
using UnityEngine;
using UnityEngine.Events;

public class Cowboy :  MonoBehaviour
{
    
    [SerializeField] PlayerController controller;
    
    public event Action<Cowboy> onPlayerShot;
    [SerializeField] AudioClip shotSound;
    AudioSource audioSource;
    private Animator anim;

    private bool playerShot = false;
    private bool falseStart = false;
    
    [Header("Animation Events")] 
    [SerializeField] private UnityEvent onGunFired;
    public event Action onShootAnimationComplete;
    public event Action onDeathAnimationFinished;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        controller.OnShootPressed += RaisePlayerShot;
    }

    private void OnDisable()
    {
        controller.OnShootPressed += RaisePlayerShot;
    }

    private void RaisePlayerShot()
    {
        onPlayerShot?.Invoke(this);
    }
    public void HandleDeath()
    {
        // play death animation
        anim.SetTrigger("Die");        
    }

    public void FalseStart()
    {
        if (falseStart) return;
        falseStart = true;
        anim.SetTrigger("FalseStart");
    }
    public void FalseStartFinishAnimationEvent()
    {
        falseStart = false;
    }

    public void GunFired()
    {
        // animation event
        ShootSFX();
        onGunFired?.Invoke();
    }


    public void FinishShootAnimation()
    {
        // animation event
        onShootAnimationComplete?.Invoke();
    }

    public void DeathAnimationFinished()
    {
        // animation event
        onDeathAnimationFinished?.Invoke();
    }
    
    public void HandleShoot()
    {
        anim.SetTrigger("Shoot");   
    }
    
    
    private void ShootSFX()
    {
        audioSource.PlayOneShot(shotSound);
    }

    public void BeginShooting()
    {
        controller.BeginShooting();
    }

    public void FalseShotChance(float timer)
    {
        controller.TryFalseStart(timer);
    }
}

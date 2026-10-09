using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private InputAction shootButton;
    [SerializeField] private PlayerController otherPlayer;
    
    public event Action<PlayerController> onPlayerShot;
    [SerializeField] AudioClip shotSound;
    AudioSource audioSource;
    private Animator anim;

    private bool playerShot = false;
    private bool falseStart = false;
    
    [Header("Animation Events")] 
    [SerializeField] private UnityEvent onGunFired;
    [SerializeField] private UnityEvent onShootAnimationComplete;
    [SerializeField] private UnityEvent onDeathAnimationFinished;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        shootButton.Enable();
    }

    private void OnDisable()
    {
        shootButton.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (shootButton.WasPerformedThisFrame())
        {
            RaisePlayerShot();
        }
        
    }

    protected void RaisePlayerShot()
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

    public void GunFired()
    {
        ShootSFX();
        onGunFired?.Invoke();
        
    }

    public void FalseStartFinish()
    {
        falseStart = false;
    }
    public void FinishShootAnimation()
    {
        onShootAnimationComplete?.Invoke();
    }

    public void DeathAnimationFinished()
    {
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
}

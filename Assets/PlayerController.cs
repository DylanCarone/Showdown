using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputAction shootButton;

    public event Action OnShootPressed;
    
    private void OnEnable()
    {
        shootButton.performed += HandleShootPressed;
        shootButton.Enable();
    }


    private void OnDisable()
    {
        shootButton.performed -= HandleShootPressed;
        shootButton.Disable();
    }

    public void HandleShootPressed(InputAction.CallbackContext context)
    {
        TriggerShot();
    }

    public void TriggerShot()
    {
        OnShootPressed?.Invoke();
    }

    public virtual void BeginShooting()
    {
        // the player controller character will control their own shooting
    }

    public virtual void TryFalseStart(float timer)
    {
        // the player controller character will control their own shooting
    }
    
}

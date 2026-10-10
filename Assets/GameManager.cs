using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private GameState gameState;
    
    [SerializeField] private Cowboy player1;

    [SerializeField] private Cowboy player2;
    [SerializeField] UIManager uiManager;

    private AudioSource aud;

    private bool gameEnded = false;

    private void Awake()
    {
        aud =  GetComponent<AudioSource>();
    }


    private void Start()
    {
        gameState = GameState.Waiting;
        StartCoroutine(CountdownTimer());
    }

    private void OnEnable()
    {
        player1.onPlayerShot += HandleShotAttempt;
        player2.onPlayerShot += HandleShotAttempt;
        player1.onDeathAnimationFinished += ResolveMatch;
        player1.onShootAnimationComplete += ResolveMatch;
        player2.onDeathAnimationFinished += ResolveMatch;
        player2.onShootAnimationComplete += ResolveMatch;
    }

    private void OnDisable()
    {
        player1.onPlayerShot -= HandleShotAttempt;
        player2.onPlayerShot -= HandleShotAttempt;
        player1.onDeathAnimationFinished -= ResolveMatch;
        player1.onShootAnimationComplete -= ResolveMatch;
        player2.onDeathAnimationFinished -= ResolveMatch;
        player2.onShootAnimationComplete -= ResolveMatch;
    }

    void HandleShotAttempt(Cowboy player)
    {
        switch (gameState)
        {
            case GameState.Waiting:
                player.FalseStart();
                break;
            case GameState.Shooting:
                HandleShoot(player);
                break;
        }
    }

    void HandleShoot(Cowboy player)
    {
        player.HandleShoot();
    }

    
    public void ResolveMatch()
    {
        if (gameState == GameState.End)
            return;
        gameState = GameState.End;
        aud.Play();
        uiManager.SetRestartText();
        
    }

    private void Update()
    {
        if (gameState == GameState.End)
        {
            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }


    IEnumerator CountdownTimer()
    {
        var roundTimer = Random.Range(3, 6);
        player1.FalseShotChance(roundTimer);
        player2.FalseShotChance(roundTimer);
        yield return new WaitForSeconds(roundTimer);
        
        uiManager.SetReadyText("SHOOT");
        gameState = GameState.Shooting;
        
        player1.BeginShooting();
        player2.BeginShooting();
    }



}


public enum GameState
{
    Waiting,
    Shooting,
    Resolution,
    End
}
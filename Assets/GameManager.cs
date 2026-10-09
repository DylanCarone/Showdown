using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private GameState gameState;
    
    [SerializeField] private PlayerController player1;

    [SerializeField] private CPUController player2;
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
    }

    private void OnDisable()
    {
        player1.onPlayerShot -= HandleShotAttempt;
        player2.onPlayerShot -= HandleShotAttempt;
    }

    void HandleShotAttempt(PlayerController player)
    {
        switch (gameState)
        {
            case GameState.Waiting:
                player.FalseStart();
                break;
            case GameState.Shooting:
                HandleShoot(player);
                break;
            default:
                Debug.Log("Cannot shoot anymore");
                break;
        }
        
    }

    void HandleShoot(PlayerController player)
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
        yield return new WaitForSeconds(Random.Range(3, 6));
        
        uiManager.SetReadyText("SHOOT");
        gameState = GameState.Shooting;
        
        player2.CPUShoot();
    }



}


public enum GameState
{
    Waiting,
    Shooting,
    Resolution,
    End
}
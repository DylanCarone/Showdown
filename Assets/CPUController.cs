using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CPUController : PlayerController
{
    private float minDelay;
    private float maxDelay;
    private float fastStartChance;
    [SerializeField] private Dictionary<CPUDifficulty, Vector3> difficuties;
    [SerializeField] CPUDifficulty currentDifficulty;

    
    
    private void Start()
    {
        if (difficuties.TryGetValue(currentDifficulty, out Vector3 information))
        {
            minDelay = information.x;
            maxDelay = information.y;
            fastStartChance = information.z;
        }
    }

    public override void TryFalseStart(float timer)
    {
        if (Random.value < fastStartChance)
        {
            StartCoroutine(FalseStartRoutine(timer));
        }
    }

    IEnumerator FalseStartRoutine(float timer)
    {
        yield return new WaitForSeconds(timer - 0.2f);
        TriggerShot();
    }

    public override void BeginShooting()
    {
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        TriggerShot();
    }
    
    
}


public enum CPUDifficulty
{
    Easy,
    Normal,
    Hard,
    Insane
}
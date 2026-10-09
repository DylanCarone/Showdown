using System.Collections;
using UnityEngine;

public class CPUController : PlayerController
{
    [SerializeField] float minDelay = 0.25f;
    [SerializeField] float maxDelay = 0.75f;


    public void CPUShoot()
    {
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        RaisePlayerShot();
    }
}

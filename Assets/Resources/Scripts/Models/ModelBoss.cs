using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelBoss : ModelEnemy, IShoot
{
    public GameObject[] cannons;
    public ModelLaser laser;
    public ShootingPattern[] shootingPatterns;

    protected override void Start()
    {
        if (sound == null) sound = FindObjectOfType<SoundManager>();
        if (secondaryControllers.Length > 1)
            StartCoroutine(ChangeAI());
        _boxC = GetComponent<BoxCollider>();

        for (int i = 0; i < shootingPatterns.Length; i++)
        {
            shootingPatterns[i] = shootingPatterns[i].Clone();
            shootingPatterns[i].SetShooter(this);
        }

        StartCoroutine(SwapShootingPatterns(Random.Range(0, shootingPatterns.Length)));
    }

    IEnumerator SwapShootingPatterns(int i)
    {
        yield return new WaitForSeconds(shotCooldown);
        Shoot();
        myShootingPattern = shootingPatterns[i];
        StartCoroutine(SwapShootingPatterns(Random.Range(0, shootingPatterns.Length)));
    }

    protected override void Death()
    {
        base.Death();
        EventManager.TriggerEvent("Win");
    }
}
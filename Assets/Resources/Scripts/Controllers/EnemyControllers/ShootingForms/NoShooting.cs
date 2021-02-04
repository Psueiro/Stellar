using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controllers/AI/Shooting Patterns/No Shooting")]
public class NoShooting : ShootingPattern
{
    public float cooldown;
    ModelEnemy _shooter;
    public override ShootingPattern Clone()
    {
        NoShooting clone = CreateInstance("NoShooting") as NoShooting;
        return this;
    }

    public override ShootingPattern SetShooter(ModelEnemy shooter)
    {
        _shooter = shooter;
        return this;
    }

    public override void Shoot()
    {
        _shooter.shotCooldown = cooldown;
    }
}
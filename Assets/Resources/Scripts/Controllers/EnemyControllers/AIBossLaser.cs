using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controllers/AI/Shooting Patterns/Boss Laser")]
public class AIBossLaser : ShootingPattern
{
    ModelBoss _shooter;
    public float cooldown;
    public float duration;
    public Vector3[] myDirections;
    public Vector3[] myRotationDirections;
    public AudioClip sound;

    public override ShootingPattern Clone()
    {
        AIBossLaser clone = CreateInstance("AIBossLaser") as AIBossLaser;
        clone.cooldown = cooldown;
        clone.myDirections = myDirections;
        clone.myRotationDirections = myRotationDirections;
        clone.duration = duration;
        clone.sound = sound;
        return clone;
    }

    public override ShootingPattern SetShooter(ModelEnemy shooter)
    {
        _shooter = shooter as ModelBoss;
        return this;
    }

    public override void Shoot()
    {
        _shooter.shotCooldown = cooldown;
        ModelLaser myLaser = _shooter.laser;
        _shooter.sound.Play(sound);
        myLaser.damage = _shooter.damage;
        int i = Random.Range(0, myDirections.Length);
        myLaser.transform.forward = myDirections[i];
        myLaser.gameObject.SetActive(true);
        (myLaser.controller as AIRotate).dir = myRotationDirections[i];
        myLaser.StartCoroutine(myLaser.Disabler(duration));
    }
}
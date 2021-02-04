using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controllers/AI/Shooting Patterns/Boss Missile")]
public class AIBossMissiles : ShootingPattern
{
    ModelBoss _shooter;
    public float cooldown;
    public float distance;
    public ModelMissile bullet;
    public Vector2[] positions;
    public AudioClip sound;

    public override ShootingPattern Clone()
    {
        AIBossMissiles clone = CreateInstance("AIBossMissiles") as AIBossMissiles;
        clone.cooldown = cooldown;
        clone.positions = positions;
        clone.distance = distance;
        clone.bullet = bullet;
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
        for (int i = 0; i < positions.Length; i++)
        {
            ModelMissile newBullet = Instantiate(bullet);
            _shooter.sound.Play(sound);
            newBullet.transform.position = new Vector3(_shooter.transform.position.x + positions[i].x, _shooter.transform.position.y + positions[i].y, _shooter.transform.position.z + distance);
            newBullet.transform.forward = RemoveZ(newBullet.transform.position - _shooter.transform.position).normalized;
            newBullet.target = _shooter.player;
        }
    }

    Vector3 RemoveZ(Vector3 v)
    {
        Vector3 newV = new Vector3(v.x, v.y, 0);
        return newV;
    }

}
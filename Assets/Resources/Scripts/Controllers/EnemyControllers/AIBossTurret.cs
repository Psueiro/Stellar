using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Controllers/AI/Shooting Patterns/BossTurret")]
public class AIBossTurret : ShootingPattern
{
    ModelBoss _shooter;
    public float cooldown;
    public float delays;
    public float damage;
    public ModelBullet bullet;
    public int rounds;
    public Vector3[] cannonVariations;
    public AudioClip sound;

    public override ShootingPattern Clone()
    {
        AIBossTurret clone = CreateInstance("AIBossTurret") as AIBossTurret;
        clone.cooldown = cooldown;
        clone.bullet = bullet;
        clone.delays = delays;
        clone.rounds = rounds;
        clone.damage = damage;
        clone.cannonVariations = cannonVariations;
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
        for (int k = 0; k < rounds; k++)
        {
            for (int j = 0; j < cannonVariations.Length; j++)
            {
                for (int i = 0; i < _shooter.cannons.Length; i++)
                {
                    _shooter.StartCoroutine(CallBullets((delays / cannonVariations.Length) * j + delays * k, _shooter.cannons[i], cannonVariations[j]));
                }
            }
        }
    }

    IEnumerator CallBullets(float i, GameObject originalCannon, Vector3 cannon)
    {
        yield return new WaitForSeconds(i);
        ModelBullet newBullet = Instantiate(bullet);
        _shooter.sound.Play(sound);
        newBullet.damage = damage;
        newBullet.shooter = _shooter;
        newBullet.transform.position = originalCannon.transform.position + cannon;
        newBullet.transform.forward = (_shooter.player.transform.position - (newBullet.transform.position + cannon)).normalized;
    }
}
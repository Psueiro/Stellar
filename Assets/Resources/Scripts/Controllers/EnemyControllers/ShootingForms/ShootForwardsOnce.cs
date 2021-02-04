using UnityEngine;

[CreateAssetMenu(menuName = "Controllers/AI/Shooting Patterns/ShootForwardsOnce")]
public class ShootForwardsOnce : ShootingPattern
{
    public float cooldown;

    public override ShootingPattern Clone()
    {
        ShootForwardsOnce clone = CreateInstance("ShootForwardsOnce") as ShootForwardsOnce;
        clone.cooldown = cooldown;
        return clone;
    }

    public override ShootingPattern SetShooter(ModelEnemy shooter)
    {
        myShooter = shooter;
        return this;
    }

    public override void Shoot()
    {
        ModelBullet newBullet = myShooter.bullet;
        newBullet.transform.position = myShooter.transform.position + Multiplier(myShooter.transform.forward, myShooter.cannonPositions[0]);
        newBullet.transform.forward = myShooter.transform.forward;
        myShooter.shotCooldown = cooldown;
        newBullet.SetBulletAttributes(myShooter.damage,myShooter);
        Instantiate(newBullet);
    }

    Vector3 Multiplier(Vector3 a, Vector3 b)
    {
        Vector3 newV = new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        return newV;
    }
}
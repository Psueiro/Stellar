using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootingPattern : ScriptableObject
{
    public ModelEnemy myShooter;
    public abstract void Shoot();
    public abstract ShootingPattern Clone();
    public abstract ShootingPattern SetShooter(ModelEnemy shooter);
}

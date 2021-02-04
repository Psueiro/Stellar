using UnityEngine;
using System;

[Serializable]
public class EnemySpawnAttributes
{
    public Vector3 spawnPos;
    public ShootingPattern shootingPattern;
    public EnemyAttributes enemyAttributes;
    public ControllerWrapper[] enemyController;
    public float[] aIChangeTimer;
    public Quaternion spawnRot;
    public float nextSpawnDelay;
}

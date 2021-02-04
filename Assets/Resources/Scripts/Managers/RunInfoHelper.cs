using UnityEngine;

public class RunInfoHelper
{
    public bool cont;
    public int lives;
    public int enemyNumber;
    public float power;
    public float health;
    public string levelName;
    public Vector3 position;

    public Vector3[] bulletPositions;
    public Vector3[] bulletRotations;
    public bool[] isShooterPlayer;
    public float[] bulletDamage;

    //ModelEnemy[] allEnemies;
    public Vector3[] enemyPositions;
    public Vector3[] enemyRotations;
    public float[] enemyHealth;
    public int[] enemyID;
    public int[] enemyAIID;
}
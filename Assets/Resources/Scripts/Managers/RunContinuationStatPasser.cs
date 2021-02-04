using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunContinuationStatPasser : MonoBehaviour
{
    public int lives;
    public int enemyNumber;
    public float power;
    public float health;
    public Vector3 position;
    public List<ModelEnemy> enemies;

    public ModelBullet bullet;
    public float[] bulletDamage;
    public Vector3[] bulletPosition;
    public Vector3[] bulletRotation;
    public bool[] bulletSpawner;

    public ModelEnemy enemy;
    public Vector3[] enemyPosition;
    public Vector3[] enemyRotation;
    public float[] enemyHealth;
    public int[] enemyID;
    public int[] enemyAIID;

    public float crosshairDistance;
    private void Start()
    {
        DontDestroyOnLoad(this);
        StartCoroutine(FindPlayer());
    }

    IEnumerator FindPlayer()
    {
        yield return new WaitForFixedUpdate();
        if(!FindObjectOfType<ModelPlayer>())
        {
            StartCoroutine(FindPlayer());
        }
        else
        {
            ModelPlayer _player = FindObjectOfType<ModelPlayer>();
            _player.lives = lives;
            _player.damage = power;
            _player.health = health;
            _player.transform.position = position;
            ModelCrosshair _crosshair = FindObjectOfType<ModelCrosshair>();
            _crosshair.transform.position = position + new Vector3(0,0,crosshairDistance);
            EnemySpawnManager _manager = FindObjectOfType<EnemySpawnManager>();
            _manager.currentEnemy = enemyNumber;
            //get currentspawn wait time

            //get current info from enemies and spawn them

            for (int i = 0; i < bulletDamage.Length; i++)
            {
                ModelBullet newBullet = Instantiate(bullet);
                newBullet.transform.position = bulletPosition[i];
                if(bulletSpawner[i])
                newBullet.shooter = _player;
                newBullet.damage = bulletDamage[i];
            }

            for (int i = 0; i < enemyPosition.Length; i++)
            {
                ModelEnemy newEnemy = Instantiate(enemy);
                newEnemy.transform.position = enemyPosition[i];
                newEnemy.transform.forward = enemyRotation[i];
                newEnemy.health = enemyHealth[i];
                newEnemy.id = enemyID[i];
                newEnemy.SetAttributes(_manager.mySpawnAttributes[newEnemy.id].enemyAttributes);
                newEnemy.SetAI(_manager.mySpawnAttributes[newEnemy.id].enemyController[enemyAIID[i]]);

                if (_manager.mySpawnAttributes[newEnemy.id].enemyController.Length > 1)
                    newEnemy.SetSecondaryAIs(_manager.mySpawnAttributes[newEnemy.id].enemyController, _manager.mySpawnAttributes[newEnemy.id].aIChangeTimer);

                if (_manager.mySpawnAttributes[newEnemy.id].shootingPattern)
                    newEnemy.SetShootingPattern(_manager.mySpawnAttributes[newEnemy.id].shootingPattern);
                newEnemy.player = _player;
                newEnemy.controller = newEnemy.secondaryControllers[enemyAIID[i]];
            }

            Destroy(gameObject);
        }
    }
}
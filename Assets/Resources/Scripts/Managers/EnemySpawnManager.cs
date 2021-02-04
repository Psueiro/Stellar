using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public ModelPlayer player;
    public float firstSpawnDelay;
    public EnemySpawnAttributes[] mySpawnAttributes;
    public ModelEnemy enemy;
    public int currentEnemy;

    public void Start()
    {
        StartCoroutine(SpawnEnemies(firstSpawnDelay));
    }

    IEnumerator SpawnEnemies(float i)
    {
        yield return new WaitForSeconds(i);
        if (currentEnemy < mySpawnAttributes.Length)
        {
            ModelEnemy newEnemy = Instantiate(enemy);
            Vector3 _newEnemyPos = new Vector3(mySpawnAttributes[currentEnemy].spawnPos.x, mySpawnAttributes[currentEnemy].spawnPos.y, player.transform.position.z + mySpawnAttributes[currentEnemy].spawnPos.z);
            newEnemy.transform.position = _newEnemyPos;
            newEnemy.transform.rotation = Quaternion.Euler(mySpawnAttributes[currentEnemy].spawnRot.x, mySpawnAttributes[currentEnemy].spawnRot.y, mySpawnAttributes[currentEnemy].spawnRot.z);
            newEnemy.SetAttributes(mySpawnAttributes[currentEnemy].enemyAttributes);        
            newEnemy.SetAI(mySpawnAttributes[currentEnemy].enemyController[0]);
            newEnemy.id = currentEnemy;
            if (mySpawnAttributes[currentEnemy].enemyController.Length > 1)            
                newEnemy.SetSecondaryAIs(mySpawnAttributes[currentEnemy].enemyController, mySpawnAttributes[currentEnemy].aIChangeTimer);

            if(mySpawnAttributes[currentEnemy].shootingPattern)
            newEnemy.SetShootingPattern(mySpawnAttributes[currentEnemy].shootingPattern);
            newEnemy.player = player;
            StartCoroutine(SpawnEnemies(mySpawnAttributes[currentEnemy].nextSpawnDelay));
            currentEnemy++;
        }
    }
}
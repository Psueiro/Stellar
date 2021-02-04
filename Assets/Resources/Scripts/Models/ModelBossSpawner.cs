using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelBossSpawner : MonoBehaviour
{
    public ModelEnemy modelBoss;
    public EnemySpawnAttributes mySpawnAttributes;

    void Spawn(ModelPlayer player)
    {

        ModelEnemy _newBoss = Instantiate(modelBoss);
        _newBoss.player = player;
        player.forwardSpeed = 0;
        _newBoss.transform.position = new Vector3(0, mySpawnAttributes.spawnPos.y, player.transform.position.z + mySpawnAttributes.spawnPos.z);
        _newBoss.SetAI(mySpawnAttributes.enemyController[0]);
        if (mySpawnAttributes.enemyController.Length > 1)
            _newBoss.SetSecondaryAIs(mySpawnAttributes.enemyController, mySpawnAttributes.aIChangeTimer);


        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.GetComponent<ModelPlayer>())
        {
            Spawn(c.gameObject.GetComponent<ModelPlayer>());
        }
    }

    private void OnCollisionStay(Collision c)
    {
        if (c.gameObject.GetComponent<ModelPlayer>())
        {
            Spawn(c.gameObject.GetComponent<ModelPlayer>());
        }
    }
}
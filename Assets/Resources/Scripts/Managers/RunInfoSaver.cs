using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class RunInfoSaver : MonoBehaviour
{
    int _lives;
    int _enemyNumber;
    float _power;
    float _health;
    string _levelName;
    string _path;
    string _data;
    Vector3 _position;
    RunInfoHelper _helper;

    ////bullets
    Vector3[] _bulletPos;
    Vector3[] _bulletRot;
    bool[] _isShooterPlayer;
    float[] _bulletDamage;

    ////enemies
    Vector3[] _enemyPositions;
    Vector3[] _enemyRotations;
    float[] _enemyHealth;
    int[] _enemyID;
    int[] _enemyAIID;

    private void Start()
    {
        _path = Application.persistentDataPath + "/run.json";
    }

    public void GatherRunInfo()
    {
        ModelPlayer _model = FindObjectOfType<ModelPlayer>();
        _lives = _model.lives;
        _power = _model.damage;
        _health = _model.health;
        _levelName = SceneManager.GetActiveScene().name;
        _position = _model.transform.position;
        _enemyNumber = FindObjectOfType<EnemySpawnManager>().currentEnemy;

        ModelBullet[] allBullets = FindObjectsOfType<ModelBullet>();
        ModelEnemy[] allEnemies = FindObjectsOfType<ModelEnemy>();

        _bulletPos = new Vector3[allBullets.Length];
        _bulletRot = new Vector3[allBullets.Length];
        _isShooterPlayer = new bool[allBullets.Length];
        _bulletDamage = new float[allBullets.Length];

        for (int i = 0; i < allBullets.Length; i++)
        {
            _bulletPos[i] = allBullets[i].transform.position;
            _bulletRot[i] = allBullets[i].transform.forward;

            if (allBullets[i].shooter is ModelPlayer)
                _isShooterPlayer[i] = true;
            else _isShooterPlayer[i] = false;

            _bulletDamage[i] = allBullets[i].damage;
        }

        _enemyPositions = new Vector3[allEnemies.Length];
        _enemyRotations = new Vector3[allEnemies.Length];
        _enemyHealth = new float[allEnemies.Length];
        _enemyID = new int[allEnemies.Length];
        _enemyAIID = new int[allEnemies.Length];

        for (int i = 0; i < allEnemies.Length; i++)
        {
            _enemyPositions[i] = allEnemies[i].transform.position;
            _enemyRotations[i] = allEnemies[i].transform.forward;
            _enemyHealth[i] = allEnemies[i].health;
            _enemyID[i] = allEnemies[i].id;

            int _AIID = 0;
            //chequeo
            for (int j = 0; j < allEnemies[i].secondaryControllers.Length; j++)
            {
                if (allEnemies[i].controller == allEnemies[i].secondaryControllers[j])
                    _AIID = j;
            }
            _enemyAIID[i] = _AIID;
        }

        SaveRunInfo();
        GetComponent<GameMenuManager>().GoToMainMenu();
    }

    public void SaveRunInfo()
    {
        _data = File.ReadAllText(_path);
        _helper = JsonUtility.FromJson<RunInfoHelper>(_data);
        _helper.cont = true;
        _helper.lives = _lives;
        _helper.enemyNumber = _enemyNumber;
        _helper.power = _power;
        _helper.health = _health;
        _helper.levelName = _levelName;
        _helper.position = _position;

        _helper.bulletPositions = _bulletPos;
        _helper.bulletRotations = _bulletRot;
        _helper.bulletDamage = _bulletDamage;
        _helper.isShooterPlayer = _isShooterPlayer;

        _helper.enemyPositions = _enemyPositions;
        _helper.enemyRotations = _enemyRotations;
        _helper.enemyHealth = _enemyHealth;
        _helper.enemyID = _enemyID;
        _helper.enemyAIID = _enemyAIID;

        _data = JsonUtility.ToJson(_helper);
        File.WriteAllText(_path, _data);
    }
}
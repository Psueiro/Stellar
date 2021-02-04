using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    string _path;
    string _data;
    RunInfoHelper _helper;
    public Image popup;
    public RunContinuationStatPasser cont;
    public ModelBullet bullet;
    public ModelEnemy enemy;

    private void Start()
    {
        _path = Application.persistentDataPath + "/run.json";

        if (!System.IO.File.Exists(_path))
        {
            File.Create(_path).Dispose();
            File.WriteAllText(_path, "{ }");
        }
        _data = File.ReadAllText(_path);
        _helper = JsonUtility.FromJson<RunInfoHelper>(_data);
    }

    public void Play()
    {
        if (_helper != null && _helper.cont)
        {
            popup.gameObject.SetActive(true);
        }
        else
            StartGame();
    }

    public void Continue()
    {
        SceneManager.LoadScene(_helper.levelName);
        RunContinuationStatPasser _cont = Instantiate(cont);
        _cont.enemyNumber = _helper.enemyNumber;
        _cont.health = _helper.health;
        _cont.lives = _helper.lives;
        _cont.power = _helper.power;
        _cont.position = _helper.position;

        _cont.bullet = bullet;
        _cont.bulletDamage = _helper.bulletDamage;
        _cont.bulletPosition = _helper.bulletPositions;
        _cont.bulletRotation = _helper.bulletRotations;
        _cont.bulletSpawner = _helper.isShooterPlayer;

        _cont.enemy = enemy;
        _cont.enemyPosition = _helper.enemyPositions;
        _cont.enemyRotation = _helper.enemyRotations;
        _cont.enemyHealth = _helper.enemyHealth;
        _cont.enemyID = _helper.enemyID;
        _cont.enemyAIID = _helper.enemyAIID;
    }

    public void TurnOffPopup()
    {
        popup.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
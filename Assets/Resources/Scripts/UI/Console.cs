using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Console : MonoBehaviour, IUpdate
{
    public delegate void CommandAction(List<string> d);
    public Text commandText;
    public InputField inputs;
    public Scrollbar scroll;
    public ModelPlayer model;
    Dictionary<string, CommandAction> _dic = new Dictionary<string, CommandAction>();
    Dictionary<string, string> _des = new Dictionary<string, string>();

    void Awake()
    {
        UpdateManager.SubscribeToUpdateList(this);
    }

    void Start()
    {
        RegisterCommand("/help", ShowHelp, "Shows all Commands");
        RegisterCommand("/SetHealth", HealthSetter, "Sets your health");
        RegisterCommand("/SetLives", LivesSetters, "Sets your lives");
        RegisterCommand("/IgnoreCollision", IgnoreCollision, "Disables Collisions");
        RegisterCommand("/SkipLevel", SkipLevel, "Lets you skip to this level");
        RegisterCommand("/SetDamage", SetDamage, "Sets your damage");
    }

    void RegisterCommand(string name, CommandAction com, string des)
    {
        _dic.Add(name, com);
        _des.Add(name, des);
    }

    void Write(string t)
    {
        commandText.text += t + "\n";
        scroll.value = 0;
    }

    void ShowHelp(List<string> d)
    {
        Write("");
        foreach (var item in _des)
        {
            Write(item.Key + "=>" + item.Value);
        }
    }

    void Clear()
    {
        commandText.text = "";
    }

    void HealthSetter(List<string> d)
    {
        int value = int.Parse(d[0]);

        if (value > model.maxHealth)
            value = (int) model.maxHealth ;
        
        model.health = value;
    }

    void LivesSetters(List<string> d)
    {
        int value = int.Parse(d[0]);

        if (value > model.maxLives)
            value = (int)model.maxLives;

        if (value < 0) value = 0;

        model.lives = value;
    }

    void IgnoreCollision(List<string> d)
    {
        int i = int.Parse(d[0]);

        if (i == 0)
            model.GetComponent<BoxCollider>().enabled = false;
        else if (i == 1)
            model.GetComponent<BoxCollider>().enabled = true;
        else
            Write("Please use only 0 or 1 to set the values (0 disables it, 1 enables it)");
    }

    void SkipLevel(List<string> d)
    {
        int i = int.Parse(d[0]);

        if(i == 1)
        {
            UpdateManager.ClearUpdateList();
            SceneManager.LoadScene("Level1");
        } else if(i == 2)
        {
            UpdateManager.ClearUpdateList();
            SceneManager.LoadScene("Level2");
        }
        else if (i == 3)
        {
            UpdateManager.ClearUpdateList();
            SceneManager.LoadScene("Level3");
        }
    }

    void SetDamage(List<string> d)
    {
        int value = int.Parse(d[0]);

        if (value > model.maxDamage)
            value = (int)model.maxDamage;

        if (value < 1) value = 1;

        model.damage = value;
    }

    void CheckKey(string s)
    {

        char[] delimiter = new char[] { ' ' };
        string[] result = s.Split(delimiter);

        string myCommand = result[0];

        List<string> data = new List<string>();

        for (int i = 1; i < result.Length; i++)
        {
            data.Add(result[i]);
        }

        if (_dic.ContainsKey(myCommand))
        {
            _dic[myCommand](data);
        }
        inputs.text = "";
    }

    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return) && gameObject.activeSelf)
        {
            CheckKey(inputs.text);
        }
    }
}
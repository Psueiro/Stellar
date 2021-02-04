using System.Collections;
using UnityEngine;

public class StatPasser : MonoBehaviour
{
    public float damage;
    public bool restart;
    int _lives;
    void Start()
    {
        DontDestroyOnLoad(this);
        if (restart)
        {
            damage = FindObjectOfType<ModelPlayer>().damage;
        }
        else damage = 1;
        _lives = FindObjectOfType<ModelPlayer>().lives;
        StartCoroutine(WaitForSceneSwap());
    }

    IEnumerator WaitForSceneSwap()
    {
        yield return new WaitForFixedUpdate();
        ModelPlayer mp = FindObjectOfType<ModelPlayer>();
        if (mp)
        {
            mp.damage = damage;
            mp.lives = _lives;
        }
        Destroy(gameObject);
    }
}
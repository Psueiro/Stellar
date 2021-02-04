using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public Text amount;
    public ModelPlayer player;

    public void Update()
    {
        amount.text = player.lives.ToString();
    }
}

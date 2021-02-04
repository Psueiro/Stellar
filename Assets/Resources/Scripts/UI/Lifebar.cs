using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour
{
    public Image myImage;
    public ModelPlayer modelPlayer;

    private void Update()
    {
        myImage.fillAmount = modelPlayer.health / modelPlayer.maxHealth;
    }
}

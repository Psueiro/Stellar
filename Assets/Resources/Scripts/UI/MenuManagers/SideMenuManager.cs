using UnityEngine;
using UnityEngine.SceneManagement;

public class SideMenuManager : MonoBehaviour
{
   public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

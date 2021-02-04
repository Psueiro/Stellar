using UnityEngine;

public class AndroidUIEnabler : MonoBehaviour
{
    public GameObject[] UIElements;

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
            Activate(true);
        else
            Activate(false);
        Destroy(this);
    }

    void Activate(bool t)
    {
        for (int i = 0; i < UIElements.Length; i++)
        {
            UIElements[i].SetActive(t);
        }
    }
}

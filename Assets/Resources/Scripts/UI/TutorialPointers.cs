using UnityEngine;
using System.Collections;
using TMPro;

public class TutorialPointers : MonoBehaviour
{
    public string desktopText;
    public string androidText;
    public TextMeshProUGUI text;
    public bool control;
    public float disappearanceDelay;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (control)
        {
            if (Application.platform == RuntimePlatform.Android)
                text.text = androidText;
            else text.text = desktopText;
        }
        StartCoroutine(Disappearance());
    }

    IEnumerator Disappearance()
    {
        yield return new WaitForSeconds(disappearanceDelay);
        transform.gameObject.SetActive(false);
    }
}
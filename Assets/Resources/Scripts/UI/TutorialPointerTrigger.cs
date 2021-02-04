using UnityEngine;

public class TutorialPointerTrigger : MonoBehaviour
{
    public TutorialPointers tutorialPointer;

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.GetComponent<ModelPlayer>())
            tutorialPointer.gameObject.SetActive(true);
    }
}
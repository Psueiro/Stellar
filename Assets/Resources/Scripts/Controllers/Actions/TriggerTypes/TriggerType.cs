using UnityEngine;

public abstract class TriggerType : ScriptableObject
{
    public abstract bool CheckTrigger(KeyCode key);
}

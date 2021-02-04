using UnityEngine;

public abstract class ActionWrapper : ScriptableObject
{
    public IAction myAction;
    public abstract void SetAction();
    public abstract ActionWrapper Clone();
}

using System;
using UnityEngine;

[Serializable]
public class KeyLink
{
    public KeyCode key;
    public ActionWrapper action;
    public TriggerType triggerType;

    public bool CheckTrigger()
    {
        return triggerType.CheckTrigger(key);
    }

    public KeyLink Clone()
    {
        KeyLink clone = new KeyLink();
        clone.key = key;
        clone.action = action.Clone();
        clone.triggerType = triggerType;
        return clone;
    }
}
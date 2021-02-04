using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Triggers/Hold")]
public class OnKey : TriggerType
{
    public override bool CheckTrigger(KeyCode key)
    {
        return Input.GetKey(key);
    }
}

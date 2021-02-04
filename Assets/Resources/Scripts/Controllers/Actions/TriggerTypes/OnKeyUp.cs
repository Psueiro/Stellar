using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Triggers/Up")]
public class OnKeyUp : TriggerType
{
    public override bool CheckTrigger(KeyCode key)
    {
        return Input.GetKeyUp(key);
    }
}

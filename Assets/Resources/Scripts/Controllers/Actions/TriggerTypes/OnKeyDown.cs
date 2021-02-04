using UnityEngine;

[CreateAssetMenu (menuName ="Actions/Triggers/Down")]
public class OnKeyDown : TriggerType
{
    public override bool CheckTrigger(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }
}

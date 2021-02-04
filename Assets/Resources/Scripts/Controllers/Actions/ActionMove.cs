using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Move")]
public class ActionMove : ActionWrapper, IAction
{
    public Vector2 screenMeasurements;
    public Vector3 direction;
    ModelPlayer _model;

    public override ActionWrapper Clone()
    {
        ActionMove clone = CreateInstance("ActionMove") as ActionMove;
        clone.direction = direction;
        return clone;
    }

    public void Do()
    {
        Vector3 _clamPos = _model.transform.position;
        _clamPos.x = Mathf.Clamp(_clamPos.x, -screenMeasurements.x, screenMeasurements.x);
        _clamPos.y = Mathf.Clamp(_clamPos.y, -screenMeasurements.y, screenMeasurements.y);
        _clamPos += direction * _model.speed * Time.deltaTime;
        _model.transform.position = _clamPos;
    }

    public override void SetAction()
    {
        myAction = this;
    }

    public IAction SetModel(Model m)
    {
        _model = m as ModelPlayer;
        return this;
    }
}

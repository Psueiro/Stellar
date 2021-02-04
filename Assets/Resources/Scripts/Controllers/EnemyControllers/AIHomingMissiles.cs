using UnityEngine;

[CreateAssetMenu(menuName = "Controllers/AI/Homing Missile")]
public class AIHomingMissiles : ControllerWrapper, IController
{
    ModelMissile _model;
    public float distanceLimit;

    public override ControllerWrapper Clone()
    {
        AIHomingMissiles clone = CreateInstance("AIHomingMissiles") as AIHomingMissiles;
        clone.distanceLimit = distanceLimit;
        return clone;
    }

    public void OnUpdate()
    {
        if (Vector3.Distance(_model.transform.position, _model.target.transform.position) > distanceLimit)
        {
            _model.transform.forward = (_model.target.transform.position - _model.transform.position).normalized;
            _model.transform.position += _model.transform.forward * _model.speed * Time.deltaTime;
        }
        else
        {
            _model.controller = _model.endController;
        }
    }

    public override void SetController()
    {
        myController = this;
    }

    public IController SetModel(Model m)
    {
        _model = m as ModelMissile;
        return this;
    }
}
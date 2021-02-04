using UnityEngine;

[CreateAssetMenu(menuName = "Controllers/AI/AI Move Forward")]
public class AIMoveForward : ControllerWrapper, IController
{
    ModelEnemy _model;
    Vector3 _dir;

    public override ControllerWrapper Clone()
    {
        AIMoveForward clone = CreateInstance("AIMoveForward") as AIMoveForward;
        return clone;
    }

    public void OnUpdate()
    {
        _model.transform.position += _dir * -_model.speed * Time.deltaTime;
        _model.transform.forward = (_model.player.transform.position - _model.transform.position).normalized;
    }

    public override void SetController()
    {
        myController = this;
    }

    public IController SetModel(Model m)
    {
        _model = m as ModelEnemy;
        _dir = _model.transform.forward;
        return this;
    }
}
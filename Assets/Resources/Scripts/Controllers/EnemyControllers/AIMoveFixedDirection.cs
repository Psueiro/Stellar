using UnityEngine;

[CreateAssetMenu(menuName = "Controllers/AI/Move Directionally")]
public class AIMoveFixedDirection : ControllerWrapper, IController
{
    ModelEnemy _model;
    public Vector3 dir;

    public override ControllerWrapper Clone()
    {
        AIMoveFixedDirection clone = CreateInstance("AIMoveFixedDirection") as AIMoveFixedDirection;
        clone.dir = dir;
        return clone;
    }

    public void OnUpdate()
    {
        _model.transform.position += dir * -_model.speed * Time.deltaTime;
        _model.transform.forward = (_model.player.transform.position - _model.transform.position).normalized;
    }

    public override void SetController()
    {
        myController = this;
    }

    public IController SetModel(Model m)
    {
        _model = m as ModelEnemy;
        return this;
    }
}
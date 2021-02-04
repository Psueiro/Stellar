using UnityEngine;

[CreateAssetMenu(menuName ="Controllers/AI/Move Front")]
public class AIMoveFront : ControllerWrapper, IController
{
    Model _model;

    public override ControllerWrapper Clone()
    {
        AIMoveFront clone = CreateInstance("AIMoveFront") as AIMoveFront;
        return clone;
    }

    public void OnUpdate()
    {
        _model.transform.position += _model.transform.forward * _model.speed * Time.deltaTime;
    }

    public override void SetController()
    {
        myController = this;
    }

    public IController SetModel(Model m)
    {
        _model = m;
        return this;
    }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Controllers/AI/AI Rotate")]
public class AIRotate : ControllerWrapper, IController
{
    Model _model;
    public Vector3 dir;

    public override ControllerWrapper Clone()
    {
        AIRotate clone = CreateInstance("AIRotate") as AIRotate;
        clone.dir = dir;
        return clone;
    }

    public void OnUpdate()
    {
        _model.transform.Rotate(Converter(dir));
    }

    Vector3 Converter(Vector3 v)
    {
        Vector3 conv = new Vector3(v.x * _model.speed * Time.deltaTime, v.y * _model.speed * Time.deltaTime, 0);
        return conv;
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
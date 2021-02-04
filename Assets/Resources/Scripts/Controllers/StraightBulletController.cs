using UnityEngine;

[CreateAssetMenu(menuName ="Controllers/Straight Bullet Controller")]
public class StraightBulletController : ControllerWrapper, IController
{
    ModelBullet _model;

    public override ControllerWrapper Clone()
    {
        StraightBulletController clone = CreateInstance("StraightBulletController") as StraightBulletController;
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
        _model = m as ModelBullet;
        return this;
    }
}

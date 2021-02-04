using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Shoot")]
public class ActionShoot : ActionWrapper, IAction
{
    Model _model;
    IShoot _shooter;
    BoxCollider _boxC;
    Vector3 _direction;

    public override ActionWrapper Clone()
    {
        ActionShoot clone = CreateInstance("ActionShoot") as ActionShoot;
        return clone;
    }

    public void Do()
    {
        if (_shooter == null) return;

        _shooter.Shoot();
    }

    public ActionShoot SetDirection(Vector3 dir)
    {
        _direction = dir;
        return this;
    }

    public override void SetAction()
    {
        myAction = this;
    }

    public IAction SetModel(Model m)
    {
        _model = m;
        _boxC = _model.GetComponent<BoxCollider>();
        _shooter = m as IShoot;
        return this;
    }
}
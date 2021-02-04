using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controllers/AI/AI Move Sideways")]
public class AIMoveSideways : ControllerWrapper, IController
{
    ModelEnemy _model;
    Vector3 _dir;
    public float dir;

    public override ControllerWrapper Clone()
    {
        AIMoveSideways clone = CreateInstance("AIMoveSideways") as AIMoveSideways;
        clone.dir = dir;
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
        _dir = _model.transform.right * dir;
        return this;
    }
}
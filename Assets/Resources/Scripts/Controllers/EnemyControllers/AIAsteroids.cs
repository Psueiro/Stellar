using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu (menuName ="Controllers/AI/Asteroids")]
public class AIAsteroids : ControllerWrapper, IController
{
    Model _model;
    Vector3 _myRotationDirection;
    public Vector3 myDirection;

    public override ControllerWrapper Clone()
    {
        AIAsteroids clone = CreateInstance("AIAsteroids") as AIAsteroids;
        clone.myDirection = myDirection;
        return clone;
    }

    public void OnUpdate()
    {
        _model.transform.Rotate(_myRotationDirection * _model.speed * Time.deltaTime);
        _model.transform.position += myDirection * _model.speed * Time.deltaTime;
    }

    public override void SetController()
    {
        myController = this;
        _myRotationDirection = new Vector3(Random.Range(-1, 0), Random.Range(-1, 0), Random.Range(-1, 0));
    }

    public IController SetModel(Model m)
    {
        _model = m;
        return this;
    }
}

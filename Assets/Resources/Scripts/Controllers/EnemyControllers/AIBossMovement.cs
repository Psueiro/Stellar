using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controllers/AI/BossMovement")]
public class AIBossMovement : ControllerWrapper,IController
{
    Model _model;
    public Vector2 amplitude;
    public Vector2 frequency;
    public Vector2 curve;
    float _timer;

    public override ControllerWrapper Clone()
    {
        AIBossMovement clone = CreateInstance("AIBossMovement") as AIBossMovement;
        clone.amplitude = amplitude;
        clone.curve = curve;
        clone.frequency = frequency;
        return clone;
    }

    public void OnUpdate()
    {
        _timer += Time.deltaTime;
        _model.transform.position += new Vector3(Mathf.Cos(Mathf.PI * _timer * frequency.x + curve.x) * amplitude.x, Mathf.Sin(Mathf.PI * _timer * frequency.y + curve.y) * amplitude.y, 0) * Time.deltaTime;
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

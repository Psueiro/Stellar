using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Controllers/Player/Mobile")]
public class MobileController : ControllerWrapper, IController
{
    ModelPlayer _model;
    public Joy joystick; 
    public Button[] buttons;
    public ActionWrapper[] actions;
    public Vector2 screenMeasurements;

    public void OnUpdate()
    {
        Vector3 _clamPos = _model.transform.position;
        _clamPos.x = Mathf.Clamp(_clamPos.x,-screenMeasurements.x, screenMeasurements.x);
        _clamPos.y = Mathf.Clamp(_clamPos.y,-screenMeasurements.y, screenMeasurements.y);
        _clamPos += new Vector3(joystick.stickValue.x, -joystick.stickValue.y, 0) * _model.speed * Time.deltaTime;
        _model.transform.position = _clamPos;
        _model.transform.forward = ( new Vector3(_model.crosshair.transform.position.x, _model.crosshair.transform.position.y, _model.crosshair.transform.position.z) - _model.transform.position ).normalized;
        _model.transform.position += Vector3.forward * _model.forwardSpeed * Time.deltaTime;
    }

    public override void SetController()
    {
        myController = this;
        for (int i = 0; i < buttons.Length; i++)
        {
            if (actions[i].myAction == null) actions[i].SetAction();
            buttons[i].onClick.AddListener(actions[i].myAction.Do);
        }
    }

    public IController SetModel(Model m)
    {
        _model = m as ModelPlayer;
        joystick = _model.joystick;
        buttons = _model.buttons;
        actions = _model.actions;
        return this;
    }

    public override ControllerWrapper Clone()
    {
        MobileController clone = CreateInstance("MobileController") as MobileController;
        clone.screenMeasurements = screenMeasurements;
        return clone;
    }
}
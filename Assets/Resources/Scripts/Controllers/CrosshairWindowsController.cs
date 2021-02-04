using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controllers/Crosshair Windows")]
public class CrosshairWindowsController : ControllerWrapper, IController
{
    public KeyLink[] keyLinks;
    ModelCrosshair _model;
    public float speed;

    public override ControllerWrapper Clone()
    {
        CrosshairWindowsController clone = CreateInstance("CrosshairWindowsController") as CrosshairWindowsController;
        clone.keyLinks = keyLinks;
        clone.speed = speed;
        return clone;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < keyLinks.Length; i++)
        {
            CheckAction(keyLinks[i]);
        }

        _model.transform.position += Vector3.forward * _model.player.forwardSpeed * Time.deltaTime;
    }

    void CheckAction(KeyLink link)
    {
        if (link.CheckTrigger())
        {
            (link.action as IAction).Do();
        }else
            _model.transform.position += new Vector3(_model.player.transform.position.x - _model.transform.position.x, _model.player.transform.position.y - _model.transform.position.y, 0) * speed / keyLinks.Length * Time.deltaTime;

    }

    public override void SetController()
    {
        myController = this;
        for (int i = 0; i < keyLinks.Length; i++)
        {
            keyLinks[i].action.SetAction();
            (keyLinks[i].action as IAction).SetModel(_model);
        }
    }

    public IController SetModel(Model m)
    {
        _model = m as ModelCrosshair;
        return this;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controllers/Player/Windows")]
public class WindowsController : ControllerWrapper, IController
{
    public KeyLink[] keyLinks;
    ModelPlayer _model;

    public override ControllerWrapper Clone()
    {
        WindowsController clone = CreateInstance("WindowsController") as WindowsController;
        for (int i = 0; i < keyLinks.Length; i++)
        {
            keyLinks[i].Clone();
        }
        clone.keyLinks = keyLinks;
        return clone;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < keyLinks.Length; i++)
        {
            CheckAction(keyLinks[i]);
        }

        _model.transform.position += Vector3.forward * _model.forwardSpeed * Time.deltaTime;
        _model.transform.forward = (new Vector3(_model.crosshair.transform.position.x, _model.crosshair.transform.position.y, _model.crosshair.transform.position.z) - _model.transform.position).normalized;
    }

    void CheckAction(KeyLink link)
    {
        if(link.CheckTrigger())
        {
           (link.action as IAction).Do();
        }
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
        _model = m as ModelPlayer;
        return this;
    }
}
using UnityEngine;


public class ModelCrosshair : Model, IUpdate
{
    public Joy joystick;
    public ControllerWrapper mobileController;
    public ControllerWrapper windowsController;
    public float axisSpeed;
    public ModelPlayer player;

    protected override void Awake()
    {
        base.Awake();
        mobileController = mobileController.Clone();
        (mobileController as IController).SetModel(this);
        if (mobileController.myController == null) mobileController.SetController();

        windowsController = windowsController.Clone();
        (windowsController as IController).SetModel(this);
        if (windowsController.myController == null) windowsController.SetController();

        if (Application.platform == RuntimePlatform.Android)
            controller = mobileController;
        else
            controller = windowsController;
    }
}

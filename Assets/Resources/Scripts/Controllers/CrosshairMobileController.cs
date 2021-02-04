using UnityEngine;

[CreateAssetMenu(menuName = "Controllers/Crosshair Mobile")]
public class CrosshairMobileController : ControllerWrapper, IController
{
    public Vector2 screenMeasurements;
    ModelCrosshair _model;
    ModelPlayer _player;
    public float distanceTreshold;
    Joy joystick;
    public float speed;

    public override ControllerWrapper Clone()
    {
        CrosshairMobileController clone = CreateInstance("CrosshairMobileController") as CrosshairMobileController;
        clone.speed = speed;
        clone.screenMeasurements = screenMeasurements;
        clone.distanceTreshold = distanceTreshold;
        return clone;
    }

    public void OnUpdate()
    {
        float dis = Vector3.Distance(ZRemover(_model.transform.position), ZRemover(_player.transform.position));

        if (joystick.stickValue != Vector3.zero && dis < distanceTreshold)
        {
            Vector3 _clamPos = _model.transform.position;
            _clamPos.x = Mathf.Clamp(_clamPos.x, -screenMeasurements.x, screenMeasurements.x);
            _clamPos.y = Mathf.Clamp(_clamPos.y, -screenMeasurements.y, screenMeasurements.y);

            Vector3 _innerclampPos = _clamPos;
            _innerclampPos.x = Mathf.Clamp(_innerclampPos.x, _player.transform.position.x - distanceTreshold / 2, _player.transform.position.x + distanceTreshold / 2);
            _innerclampPos.y = Mathf.Clamp(_innerclampPos.y, _player.transform.position.y - distanceTreshold / 2, _player.transform.position.y + distanceTreshold / 2);

            _innerclampPos += new Vector3(joystick.stickValue.x, -joystick.stickValue.y, 0) * _model.axisSpeed * Time.deltaTime;
            _model.transform.position = _innerclampPos;
        }
        else
            _model.transform.position += new Vector3(_model.player.transform.position.x - _model.transform.position.x, _model.player.transform.position.y - _model.transform.position.y, 0) * speed * Time.deltaTime;

        _model.transform.position += Vector3.forward * _player.forwardSpeed * Time.deltaTime;
    }

    Vector3 ZRemover(Vector3 v)
    {
        Vector3 newV = new Vector3(v.x, v.y, 0);
        return newV;
    }


    public override void SetController()
    {
        myController = this;
    }

    public IController SetModel(Model m)
    {
        _model = m as ModelCrosshair;
        joystick = _model.joystick;
        _player = _model.player;
        return this;
    }
}

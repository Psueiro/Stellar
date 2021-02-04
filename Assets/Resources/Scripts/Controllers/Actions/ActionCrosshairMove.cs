using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Crosshair Move")]
public class ActionCrosshairMove : ActionWrapper, IAction
{
    public Vector2 screenMeasurements;
    public Vector3 direction;
    public float distanceTreshold;
    ModelCrosshair _model;
    ModelPlayer _player;

    public override ActionWrapper Clone()
    {
        ActionCrosshairMove clone = CreateInstance("ActionCrosshairMove") as ActionCrosshairMove;
        clone.direction = direction;
        clone.distanceTreshold = distanceTreshold;
        return clone;
    }

    public void Do()
    {
        float dis = Vector3.Distance(ZRemover(_model.transform.position), ZRemover(_player.transform.position));

        if (dis < distanceTreshold)
        {
            Vector3 _clamPos = _model.transform.position;
            _clamPos.x = Mathf.Clamp(_clamPos.x, -screenMeasurements.x, screenMeasurements.x);
            _clamPos.y = Mathf.Clamp(_clamPos.y, -screenMeasurements.y, screenMeasurements.y);

            Vector3 _innerclampPos = _clamPos;
            _innerclampPos.x = Mathf.Clamp(_innerclampPos.x, _player.transform.position.x - distanceTreshold / 2, _player.transform.position.x + distanceTreshold / 2);
            _innerclampPos.y = Mathf.Clamp(_innerclampPos.y, _player.transform.position.y - distanceTreshold / 2, _player.transform.position.y + distanceTreshold / 2);

            _innerclampPos += direction * _model.axisSpeed * Time.deltaTime;
            _model.transform.position = _innerclampPos;
        }
    }

    Vector3 ZRemover(Vector3 v)
    {
        Vector3 newV = new Vector3(v.x, v.y, 0);
        return newV;
    }

    public override void SetAction()
    {
        myAction = this;
    }

    public IAction SetModel(Model m)
    {
        _model = m as ModelCrosshair;
        _player = _model.player;
        return this;
    }
}

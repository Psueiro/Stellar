using UnityEngine;

public class ModelCamera : MonoBehaviour, IUpdate
{
    public ModelPlayer modelPlayer;
    public float distance;

    void Awake()
    {
        UpdateManager.SubscribeToUpdateList(this);
    }

    public void OnUpdate()
    {
        transform.position = ZRemover(modelPlayer.transform.position);
    }

    Vector3 ZRemover(Vector3 r)
    {
        Vector3 newZ = new Vector3(transform.position.x, transform.position.y, r.z - distance);
        return newZ;
    }
}
using UnityEngine;

public class ModelLevelFinisher : MonoBehaviour
{
    public StatPasser statPasser;
    StatPasser _myStatPasser;

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.GetComponent<ModelPlayer>())
        {
            EventManager.TriggerEvent("Win");
            if(!_myStatPasser)
            _myStatPasser = Instantiate(statPasser);
        }
    }

    private void OnCollisionStay(Collision c)
    {
        if (c.gameObject.GetComponent<ModelPlayer>())
            EventManager.TriggerEvent("Win");
        if (!_myStatPasser)
            _myStatPasser = Instantiate(statPasser);
    }
}
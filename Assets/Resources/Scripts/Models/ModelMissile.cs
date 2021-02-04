using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMissile : Model
{
    public ControllerWrapper startController;
    public ControllerWrapper homingController;
    public ControllerWrapper endController;
    public ModelPlayer target;
    public float controllerChangeSeconds;
    public float duration;

    void Start()
    {
        startController = startController.Clone();
        startController.SetController();
        (startController as IController).SetModel(this);

        homingController = homingController.Clone();
        homingController.SetController();
        (homingController as IController).SetModel(this);

        endController = endController.Clone();
        endController.SetController();
        (endController as IController).SetModel(this);

        controller = startController;

        StartCoroutine(ControllerChange());
        StartCoroutine(DestroyThis());
    }

    IEnumerator ControllerChange()
    {
        yield return new WaitForSeconds(controllerChangeSeconds);
        controller = homingController;
    }

    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(duration);
        Death();
    }

    protected override void Death()
    {
        UpdateManager.UnsubscribeToUpdateList(this);
        Destroy(gameObject);
    }

    protected override void TakeHit(float dmg)
    {
        if (_damaged) return;
        base.TakeHit(dmg);
        if (health <= 0) Death();
        StartCoroutine(RecoverFromHit());
    }

    IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(invincibilityTime);
        Transform _mychild = transform.GetChild(0);
        _mychild.gameObject.SetActive(true);
        _damaged = false;
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.layer == 9)
        {
            if (!c.gameObject.GetComponent<ModelBullet>()) return;
            ModelBullet _modelBullet = c.gameObject.GetComponent<ModelBullet>();
            if (!(_modelBullet.shooter is ModelPlayer)) return;
            TakeHit(_modelBullet.damage);
        }
    }

}
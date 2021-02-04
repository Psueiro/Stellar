using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelBullet : Model, IUpdate
{
    public float lifeSpan;
    public Model shooter;

    protected override void Awake()
    {
        base.Awake();
        controller = controller.Clone();
        (controller as IController).SetModel(this);
        controller.SetController();
    }

    void Start()
    {
        StartCoroutine(EndLifespan());
    }

    public void SetBulletAttributes(float dmg, Model sho)
    {
        damage = dmg;
        shooter = sho;
    }

    IEnumerator EndLifespan()
    {
        yield return new WaitForSeconds(lifeSpan);
        Vanish();
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.GetComponent<ModelBullet>() || c.gameObject.GetComponent<TutorialPointerTrigger>()) return;
        Vanish();
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.GetComponent<ModelBullet>() || c.gameObject.GetComponent<TutorialPointerTrigger>()) return;
        Vanish();
    }

    void Vanish()
    {
        UpdateManager.UnsubscribeToUpdateList(this);
        Destroy(gameObject);
    }

}

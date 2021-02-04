using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour, IUpdate
{
    public float health;
    public float damage;
    public float speed;
    public ControllerWrapper controller;
    public float invincibilityTime;
    protected bool _damaged;

    protected virtual void Awake()
    {
        UpdateManager.SubscribeToUpdateList(this);
    }

    public virtual void OnUpdate()
    {
        controller.myController.OnUpdate();
        if (_damaged)
        {
            DamageBehavior();
        }
    }

    protected virtual void DamageBehavior() { }

    protected virtual void TakeHit(float dmg)
    {
        health -= dmg;
        _damaged = true;
        if (health < 1) Death();
    }

    protected virtual void Death() { }
}

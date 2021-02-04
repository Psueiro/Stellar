using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelAsteroid : Model, IUpdate
{
    public int myDeathSpawnAmount;
    public ModelAsteroid myDeathSpawn;
    SphereCollider _collider;
    MeshRenderer _en;
    SoundManager _sound;
    public AudioClip dmg;
    public bool vulnerable;

    private void Start()
    {
        controller = controller.Clone();
        controller.SetController();
        (controller as IController).SetModel(this);
        _en = GetComponent<MeshRenderer>();
        _collider = GetComponent<SphereCollider>();
        if (_sound == null) _sound = FindObjectOfType<SoundManager>();
    }

    protected override void TakeHit(float f)
    {
        if (_damaged) return;
        base.TakeHit(f);
        StartCoroutine(RecoverFromHit());
        _collider.enabled = false;
    }

    protected override void DamageBehavior()
    {
        _en.enabled = !_en.enabled;
    }

    protected override void Death()
    {
        for (int i = 0; i < myDeathSpawnAmount; i++)
        {
            ModelAsteroid newAsteroids = Instantiate(myDeathSpawn);

            float _posDefiner;
            if (i % 2 == 0)
            {
                _posDefiner = -1;
            }
            else
            {
                _posDefiner = 1;
            }

            newAsteroids.transform.position = transform.position + new Vector3(_posDefiner * _collider.radius * transform.localScale.x, 0, 0);
            newAsteroids.controller = controller.Clone();

            (newAsteroids.controller as AIAsteroids).myDirection = new Vector3(_posDefiner / 2, Random.Range(-1, 1) / 2, 0);
        }
        UpdateManager.UnsubscribeToUpdateList(this);
        Destroy(gameObject);
    }

    IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(invincibilityTime);
        _en.enabled = true;
        _collider.enabled = true;
        _damaged = false;
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.layer == 9)
        {
            _sound.Play(dmg);
            if (!vulnerable) return;
            if (!c.gameObject.GetComponent<ModelBullet>()) return;
            ModelBullet _modelBullet = c.gameObject.GetComponent<ModelBullet>();
            if (!(_modelBullet.shooter is ModelPlayer)) return;
            TakeHit(_modelBullet.damage);
        }
    }
}
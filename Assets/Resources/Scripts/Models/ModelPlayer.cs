using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelPlayer : Model, IUpdate, IShoot
{
    public float maxHealth;
    public float forwardSpeed;
    public float maxDamage;
    public float defaultDamage;
    public float damageRecoil;
    public float deathSeconds;
    public int lives;
    public int maxLives;
    public Button[] buttons;
    public ActionWrapper[] actions;
    public ControllerWrapper mobileController;
    public ControllerWrapper windowsController;
    public Joy joystick;
    public ModelCrosshair crosshair;
    public ModelBullet bullet;
    public AudioClip shotClip;
    public AudioClip dmgClip;
    SoundManager _sound;
    BoxCollider _boxC;
    Rigidbody _rb;
    Animator _anim;

    protected override void Awake()
    {
        base.Awake();
        _sound = FindObjectOfType<SoundManager>();
        _anim = GetComponent<Animator>();
        mobileController = mobileController.Clone();
        windowsController = windowsController.Clone();

        (mobileController as IController).SetModel(this);
        (windowsController as IController).SetModel(this);

        if (mobileController.myController == null) mobileController.SetController();
        if (windowsController.myController == null) windowsController.SetController();

        if (Application.platform == RuntimePlatform.Android)
            controller = mobileController;
        else
            controller = windowsController;

        for (int i = 0; i < actions.Length; i++)
        {
            actions[i] = actions[i].Clone();
            actions[i].SetAction();
            actions[i].myAction.SetModel(this);
        }
        _boxC = GetComponent<BoxCollider>();
        _rb = GetComponent<Rigidbody>();
    }

    public ModelBullet Bullet()
    {
        return bullet;
    }

    protected override void TakeHit(float damage)
    {
        if (_damaged) return;
        _sound.Play(dmgClip);
        _rb.AddForce((Vector3.zero - transform.position).normalized * damageRecoil, ForceMode.Impulse);
        _boxC.enabled = false;
        _anim.Play("Hit");
        StartCoroutine(RecoverFromHit());
        base.TakeHit(damage);
    }

    IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(invincibilityTime);

        for (int i = 0; i < transform.childCount; i++)
        {
            MeshRenderer en = transform.GetChild(i).GetComponent<MeshRenderer>();
            en.enabled = true;
        }
        _damaged = false;

        _rb.angularVelocity = Vector3.zero;
        _rb.velocity = Vector3.zero;
        _boxC.enabled = true;
        _anim.Play("Idle");
    }

    protected override void DamageBehavior()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            MeshRenderer en = transform.GetChild(i).GetComponent<MeshRenderer>();
            en.enabled = !en.enabled;
        }
    }

    public void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.layer == 0)
        {
            TakeHit(defaultDamage);
        }

        if (c.gameObject.layer == 9)
        {
            if (!c.gameObject.GetComponent<ModelBullet>()) return;
            ModelBullet _modelBullet = c.gameObject.GetComponent<ModelBullet>();
            if (_modelBullet.shooter == this) return;
            else TakeHit(_modelBullet.damage);
        }

        if (c.gameObject.layer == 10)
        {
            if (!c.gameObject.GetComponent<ModelAsteroid>() && !c.gameObject.GetComponent<ModelEnemy>()) return;
            Model _modelCollision;
            if (c.gameObject.GetComponent<ModelAsteroid>())
                _modelCollision = c.gameObject.GetComponent<ModelAsteroid>();
            else _modelCollision = c.gameObject.GetComponent<ModelEnemy>();
            TakeHit(_modelCollision.damage);
        }

        if (c.gameObject.layer == 11)
        {
            if (!c.gameObject.GetComponent<ModelMissile>()) return;
            ModelMissile _modelMissile = c.gameObject.GetComponent<ModelMissile>();
             TakeHit(_modelMissile.damage);
        }
    }

    protected override void Death()
    {
        //base.Death();
        //change Controllers
        _anim.Play("Death");
        if (lives > 0)
        {
            lives--;
            EventManager.TriggerEvent("Loss");
        }
        else
        {
            EventManager.TriggerEvent("GameOver");
        }
    }

    private void OnCollisionStay(Collision c)
    {
        if (c.gameObject.layer == 0)
        {
            TakeHit(defaultDamage);
        }
    }

    public void Shoot()
    {
        _sound.Play(shotClip);
        ModelBullet newBullet = bullet;
        newBullet.transform.position = transform.position + new Vector3(0, 0, _boxC.size.z / 2);
        newBullet.transform.forward = (crosshair.transform.position - transform.position).normalized;
        newBullet.SetBulletAttributes(damage, this);
        Instantiate(newBullet);
    }
}
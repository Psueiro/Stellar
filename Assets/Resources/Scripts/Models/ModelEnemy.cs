using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelEnemy : Model, IUpdate, IShoot
{
    public float shotCooldown;
    public float[] currentAIChangeCooldown;
    public ControllerWrapper[] secondaryControllers;
    public Vector3[] cannonPositions;
    public ModelPlayer player;
    public ModelBullet bullet;
    public ShootingPattern myShootingPattern;
    public SoundManager sound;
    public AudioClip shotClip;
    public AudioClip dmgClip;
    public int id;
    int _currentAI;
    protected BoxCollider _boxC;

    protected virtual void Start()
    {
        StartCoroutine(ShootCoroutine());
        if (secondaryControllers.Length > 1)
            StartCoroutine(ChangeAI());
        if (sound == null) sound = FindObjectOfType<SoundManager>();
    }

    public void SetAttributes(EnemyAttributes a)
    {
        health = a.health;
        speed = a.speed;
        shotCooldown = a.shotCooldown;
        cannonPositions = a.cannonPositions;
        damage = a.damage;
        GameObject _model = Instantiate(a.model);
        _model.transform.parent = transform;
        _model.transform.localPosition = Vector3.zero;
        _model.transform.localScale = a.modelScale;
        _model.transform.localRotation = Quaternion.Euler(0, 0, 0);
        _boxC = GetComponent<BoxCollider>();
        _boxC.center = a.colliderCenter;
        _boxC.size = a.colliderScale;
    }

    public ModelBullet Bullet()
    {
        return bullet;
    }

    public ModelEnemy SetAI(ControllerWrapper ai)
    {
        controller = ai.Clone();
        controller.SetController();
        controller.myController.SetModel(this);
        return this;
    }

    public ModelEnemy SetSecondaryAIs(ControllerWrapper[] ai, float[] timers)
    {
        secondaryControllers = new ControllerWrapper[ai.Length];
        currentAIChangeCooldown = timers;
        for (int i = 0; i < secondaryControllers.Length; i++)
        {
            secondaryControllers[i] = ai[i].Clone();
            secondaryControllers[i].SetController();
            secondaryControllers[i].myController.SetModel(this);
        }
        return this;
    }

    public ModelEnemy SetShootingPattern(ShootingPattern pattern)
    {
        myShootingPattern = pattern.Clone();
        myShootingPattern.SetShooter(this);
        return this;
    }

    public void Shoot()
    {
        sound.Play(shotClip);
        myShootingPattern.SetShooter(this);
        myShootingPattern.Shoot();
    }

    protected IEnumerator ShootCoroutine()
    {
        yield return new WaitForSeconds(shotCooldown);
        Shoot();
        StartCoroutine(ShootCoroutine());
    }

    protected IEnumerator ChangeAI()
    {
        if (_currentAI >= secondaryControllers.Length - 1) yield break;
        yield return new WaitForSeconds(currentAIChangeCooldown[_currentAI]);
        _currentAI++;
        controller = secondaryControllers[_currentAI];
        StartCoroutine(ChangeAI());
    }

    protected override void DamageBehavior()
    {
        Transform _mychild = transform.GetChild(0);
        _mychild.gameObject.SetActive(!_mychild.gameObject.activeSelf);
    }

    protected override void TakeHit(float dmg)
    {
        if (_damaged) return;
        sound.Play(dmgClip);
        base.TakeHit(dmg);
        if (health <= 0) Death();
        StartCoroutine(RecoverFromHit());
        _boxC.enabled = false;
    }

    IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(invincibilityTime);
        Transform _mychild = transform.GetChild(0);
        _mychild.gameObject.SetActive(true);
        _damaged = false;
        _boxC.enabled = true;
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.layer == 8)
        {
            UpdateManager.UnsubscribeToUpdateList(this);
            Destroy(gameObject);
        }
    }

    protected override void Death()
    {
        //DeathAnimation;
        UpdateManager.UnsubscribeToUpdateList(this);
        Destroy(gameObject);
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
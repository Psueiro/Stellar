using System.Collections;
using UnityEngine;

public class ModelPowerUp : MonoBehaviour
{
    Animator _anim;
    BoxCollider _boxc;

    public float delayToDisappear;
    public float healthAmount;
    public float damageAmount;
    public AudioClip clip;
    SoundManager _sound;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _boxc = GetComponent<BoxCollider>();
        if (_sound == null) _sound = FindObjectOfType<SoundManager>();
    }

    private void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.GetComponent<ModelPlayer>())
        {
            ModelPlayer _player = c.gameObject.GetComponent<ModelPlayer>();
            _boxc.enabled = false;
            _boxc.enabled = false;
            _player.damage += damageAmount;
            _sound.Play(clip);
            if (_player.damage > _player.maxDamage) _player.damage = _player.maxDamage;
            _player.health += healthAmount;
            if (_player.health > _player.maxHealth) _player.health = _player.maxHealth;
            _anim.Play("Grab");
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(delayToDisappear);
        Destroy(gameObject);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCraft : MonoBehaviour, IDamageable
{
    [SerializeField] protected List<Gun> _possibleGuns = new List<Gun>();

    [SerializeField] protected float _health;

    [SerializeField] protected AudioClip _dieClip;

    [SerializeField] protected Animator _animator;
    protected const string _dieParameterName = "IsDie";
    protected const string _damageParameterName = "IsDamage";

    public Action OnDie;
    public Action OnDamage;
    protected float _maxHealth;

    public float MaxHealth => _maxHealth;

    public float Health
    {
        get { return _health; }
        set
        {
            if (Health < value || Health <= 0)
            {
                _health = 0;
                Die();
            }
            else if (value < _maxHealth)
                _health = value;
        }
    }
    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
        OnDie += () => { GameManager.Instance.ScenesController.LoadMenuScene(); };
        _maxHealth = _health;
    }


    protected virtual void Update()
    {
        Shoot();
    }

    protected virtual void Shoot()
    {
        for (int i = 0; i < _possibleGuns.Count; i++)
        {
            if (_possibleGuns[i].gameObject.activeInHierarchy.Equals(true))
                _possibleGuns[i].Shoot(ShootingSide.right);
        }
    }

    public virtual void GetDamage(float damage)
    {
        OnDamage?.Invoke();
        Health -= damage;
        StartCoroutine(DamageAniamtionProcessRoutine());
    }

    protected void Die()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        AudioManager.Instance.PlaySfx(_dieClip);

        OnDie?.Invoke();
        StartCoroutine(DieProcesRoutine());
    }

    protected virtual IEnumerator DamageAniamtionProcessRoutine()
    {
        _animator.SetBool(_damageParameterName, false);
        yield return new WaitForSeconds(0.001f);

        _animator.SetBool(_damageParameterName, true);

        AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = _animator.GetCurrentAnimatorClipInfo(0).Length / animatorStateInfo.speed;

        yield return new WaitForSeconds(animationDuration);

        _animator.SetBool(_damageParameterName, false);
    }

    protected virtual IEnumerator DieProcesRoutine()
    {
        foreach (var item in _possibleGuns)
        {
            item.gameObject.SetActive(false);
        }

        _animator.SetBool(_dieParameterName, true);

        AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = _animator.GetCurrentAnimatorClipInfo(0).Length / animatorStateInfo.speed;

        yield return new WaitForSeconds(animationDuration);

        foreach (var item in _possibleGuns)
        {
            item.gameObject.SetActive(false);
        }

        _animator.SetBool(_dieParameterName, false);

        gameObject.SetActive(false);
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}

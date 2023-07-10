using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCraft : MonoBehaviour, IDamageable
{
    [SerializeField] private List<Gun> _possibleGuns = new List<Gun>();

    [SerializeField] protected float _health;

    [SerializeField] protected Animator _animator;
    private const string _dieParameterName = "IsDie";
    private const string _damageParameterName = "IsDamage";

    public Action OnDie;
    public Action OnDamage;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

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

    protected float _maxHealth;

    private void Update()
    {
        Shoot();
        _maxHealth = _health;
    }

    private void Shoot()
    {
        for (int i = 0; i < _possibleGuns.Count; i++)
        {
            if (_possibleGuns[i].gameObject.activeInHierarchy.Equals(true))
                _possibleGuns[i].Shoot(ShootingSide.right);
        }
    }

    public void GetDamage(float damage)
    {
        OnDamage?.Invoke();
        Health -= damage;
        StartCoroutine(DamageAniamtionProcessRoutine());
    }

    private void Die()
    {
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
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Levels;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemySpacecraft : SpaceCraft
{
    [SerializeField] protected float _speed;

    protected Rigidbody2D _rigidbody;

    [Space]
    [SerializeField] protected GameObject _exhaustObject;
    private const string _explosionParameterName = "IsDie";

    
    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _maxHealth = Health;
    }

    protected override void Start()
    {
        _animator = GetComponent<Animator>();
        _maxHealth = _health;
    }

    protected void FixedUpdate()
    {
        Fly();
    }

    protected override void Shoot()
    {
        for (int i = 0; i < _possibleGuns.Count; i++)
        {
            if (_possibleGuns[i].gameObject.activeInHierarchy.Equals(true))
                _possibleGuns[i].Shoot(ShootingSide.left);
        }
    }
    public abstract void SetSettingsFromWave(WaveElement waveElement);


    protected virtual void Fly()
    {
        _rigidbody.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - 1, transform.position.y), _speed * Time.fixedDeltaTime);
    }

    public override void GetDamage(float damage)
    {

        OnDamage?.Invoke();
        Health -= damage;
    }

    protected override IEnumerator DieProcesRoutine()
    {
        if (_animator != null)
        {
            if (_exhaustObject != null)
                _exhaustObject.gameObject.SetActive(false);
            _animator.SetBool(_explosionParameterName, true);
        }
        var savedSpeed = _speed;
        _speed = 0;

        AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = animatorStateInfo.length / _animator.speed;

        yield return new WaitForSeconds(animationDuration);

        _animator.SetBool(_explosionParameterName, false);

        if (_exhaustObject != null)
            _exhaustObject.gameObject.SetActive(true);

        _speed = savedSpeed;
        gameObject.SetActive(false);
    }
}

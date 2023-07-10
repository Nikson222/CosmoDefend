using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Levels;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemySpacecraft : MonoBehaviour, IDamageable
{
    [SerializeField] protected List<Gun> _possibleGuns = new List<Gun>();
    [SerializeField] protected float _speed;

    [SerializeField] protected float _health;
    protected float _maxHealth;
    protected Rigidbody2D _rigidbody;

    [Space]
    [SerializeField] protected GameObject _exhaustObject;
    [SerializeField] protected Animator _animator;
    private const string _explosionParameterName = "IsDie";

    public Action OnDamage;
    public Action OnDie;
    
    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    protected void FixedUpdate()
    {
        Fly();
    }
    public float MaxHealth => _maxHealth;

    public float Health
    {
        get { return _health; }
        set
        {
            if (value < _maxHealth)
                _health = value;

            if (_health < 0 || _health == 0)
            {
                _health = 0;
                Die();
            }
        }
    }


    protected virtual void Update()
    {
        Shoot();
        _maxHealth = Health;
    }

    public abstract void SetSettingsFromWave(WaveElement waveElement);

    protected void Shoot()
    {
        for (int i = 0; i < _possibleGuns.Count; i++)
        {
            if (_possibleGuns[i] != null && _possibleGuns[i].gameObject.activeInHierarchy.Equals(true))
                _possibleGuns[i].Shoot(ShootingSide.left);
        }
    }

    protected virtual void Fly()
    {
        _rigidbody.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - 1, transform.position.y), _speed * Time.fixedDeltaTime);
    }

    protected virtual void Die()
    {
        OnDie?.Invoke();
        StartCoroutine(DieProcesRoutine());
    }

    public void GetDamage(float damage)
    {
        OnDamage?.Invoke();
        Health -= damage;
    }

    protected virtual IEnumerator DieProcesRoutine()
    {
        if (_animator != null)
        {
            if (_exhaustObject != null)
                _exhaustObject.gameObject.SetActive(false);
            _animator.SetBool(_explosionParameterName, true);
        }

        AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = animatorStateInfo.length / _animator.speed;

        yield return new WaitForSeconds(animationDuration);

        _animator.SetBool(_explosionParameterName, false);

        if (_exhaustObject != null)
            _exhaustObject.gameObject.SetActive(true);

        gameObject.SetActive(false);
    }
}

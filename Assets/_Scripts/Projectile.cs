using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour
{
    public Rigidbody2D _rigidbody;
    protected float _senderForce;
    protected float _senderDamage;
    protected Vector2 _shootingSide;

    [SerializeField] protected float _speed;
    protected float _savedSpeed;
    [SerializeField] protected float _damage;

    [SerializeField] protected Animator _animator;
    private const string _hitParameterName = "IsHit";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _savedSpeed = _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageableObject;

        collision.gameObject.TryGetComponent<IDamageable>(out damageableObject);

        if (damageableObject != null)
        {
            GiveDamage(damageableObject);
        }
    }

    private void OnEnable()
    {
        _speed = _savedSpeed;
    }

    public void ApplySettings(float senderForce, Vector2 position, ShootingSide shootingSide, float senderDamage = 0)
    {
        transform.position = new Vector2(position.x, position.y);
        _senderForce = senderForce;
        _senderDamage = senderDamage;

        if (shootingSide.Equals(ShootingSide.left))
            _shootingSide = Vector2.left;
        else
            _shootingSide = Vector2.right;
    }

    public abstract void Fly();

    IEnumerator disable()
    {
        yield return new WaitForSeconds(20f);
        gameObject.SetActive(false);
    }

    protected IEnumerator HitProcessRoutine()
    {
        _animator.SetBool(_hitParameterName, true);

        AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = _animator.GetCurrentAnimatorClipInfo(0).Length / animatorStateInfo.speed;

        _speed = 0;

        yield return new WaitForSeconds(animationDuration - _animator.GetAnimatorTransitionInfo(0).duration);

        _animator.SetBool(_hitParameterName, false);

        gameObject.SetActive(false);
    }

    protected void GiveDamage(IDamageable damageableObject)
    {
        damageableObject.GetDamage(_damage);

        StartCoroutine(HitProcessRoutine());
    }
}

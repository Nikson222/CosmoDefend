using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private float _shootingDelay;
    [SerializeField] private float _shootingForce;
    [SerializeField] private float _damageMultiplier;
    [SerializeField] protected AudioClip _shootClip;
    private ObjectPooler<Projectile> _pooler = new ObjectPooler<Projectile>(20);

    private bool _isReloading = false;

    private Coroutine _reloadingCoroutine;

    private void OnDisable()
    {
        if (_reloadingCoroutine != null)
            StopCoroutine(_reloadingCoroutine);
        _isReloading = false;
    }

    private void Awake()
    {
        _pooler.Init(_projectile);
    }

    public void InitNewSettings(Projectile newPrefab)
    {
        _projectile = newPrefab;
        _pooler.Init(newPrefab);
    }

    public void Shoot(ShootingSide shootingSide)
    {
        if (_isReloading)
            return;
        AudioManager.Instance.PlaySfx(_shootClip);
        if (_pooler == null)
            _pooler = new ObjectPooler<Projectile>(20);

        var projectile = _pooler.GetObject();
        projectile.ApplySettings(_shootingForce, transform.position, shootingSide, _damageMultiplier);

        _reloadingCoroutine = StartCoroutine(ReloadRoutine());

        _isReloading = true;
    }

    private IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(_shootingDelay);
        _isReloading = false;
    }
}

public enum ShootingSide
{
    left,
    right
}
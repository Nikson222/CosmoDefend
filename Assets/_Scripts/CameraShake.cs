using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Vector2 _leftUpOffset;
    [SerializeField] private Vector2 _rightDownOffset;
    [SerializeField] private float _shakeSpeed;
    [SerializeField] private float _shakeCount;
    [SerializeField] private Transform _cameraTransform;

    [Space]
    [SerializeField] private SpaceCraft _player;
    private Coroutine _shakeCoroutine;

    private void Start()
    {
        _player = FindObjectOfType<SpaceCraft>();

        _cameraTransform = Camera.main.transform;
        if (_player)
        {
            _player.OnDamage += StartShake;
            _player.OnDie += StartShake;
        }
    }

    private void StartShake()
    {
        if (_shakeCoroutine != null)
            StopCoroutine(_shakeCoroutine);
        _shakeCoroutine = StartCoroutine(Shake());
    }
    private IEnumerator Shake()
    {
        _cameraTransform.position = new Vector3(0, 0, -10);

        Vector3 savedPosition = _cameraTransform.position;

        for (int i = 0; i < _shakeCount; i++)
        {
            Vector3 targetPosition = GetRandomPositionFromOffset();
            while (!IsComeToPoint(targetPosition))
            {
                var deltaSpeed = _shakeSpeed * Time.fixedDeltaTime;
                _cameraTransform.position = Vector3.MoveTowards(_cameraTransform.position, targetPosition, deltaSpeed);
                yield return null;
            }
        }

        while (!IsComeToPoint(savedPosition))
        {
            var deltaSpeed = _shakeSpeed * Time.fixedDeltaTime;
            _cameraTransform.position = Vector3.MoveTowards(_cameraTransform.position, savedPosition, deltaSpeed);
            yield return null;
        }
    }


    private bool IsComeToPoint(Vector2 target)
    {
        Vector3 direction = target - (Vector2)transform.position;
        if (direction.magnitude <= Mathf.Epsilon)
        {
            return true;
        }
        else
            return false;
    }

    private Vector3 GetRandomPositionFromOffset()
    {
        var xPos = Random.Range(_leftUpOffset.x, _rightDownOffset.x);
        var yPOs = Random.Range(_leftUpOffset.y, _rightDownOffset.y);

        return new Vector3(xPos, yPOs, -10);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_leftUpOffset, 0.1f);
        Gizmos.DrawSphere(_rightDownOffset, 0.1f);
    }
}

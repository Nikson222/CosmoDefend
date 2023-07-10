using Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollowEnemy : EnemySpacecraft
{
    [SerializeField] private Transform _target;
    [SerializeField] Vector3 direction;
    public override void SetSettingsFromWave(WaveElement waveElement)
    {
        SetTarget(waveElement.Target);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    protected override void Fly()
    {
        if (!_target)
            base.Fly();
        else
        {
            direction = (_target.position - transform.position).normalized;
            float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float angleX = 180;
            float angleY = 180;

            if (direction.x > 0)
            {
                angleX -= 180;
                angleZ = angleZ * -1;
            }

            Quaternion targetRotation = Quaternion.Euler(angleX, angleY, angleZ);

            transform.rotation = targetRotation;

            Vector3 velocity = direction * _speed;
            _rigidbody.velocity = velocity;
        }
    }
}

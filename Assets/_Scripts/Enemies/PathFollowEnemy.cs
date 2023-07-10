using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Levels;

public class PathFollowEnemy : EnemySpacecraft
{
    private Vector2 _targetPoint;
    private int _currentPointIndex;
    [SerializeField] protected PointsPattern _pathLine;

    protected void OnEnable()
    {
        _currentPointIndex = 0;
    }

    protected override void Update()
    {
        base.Update();
        if (_pathLine)
        {
            Vector3 direction = _targetPoint - (Vector2)transform.position;
            if (direction.magnitude <= 0.01f)
            {
                if (_currentPointIndex + 1 < _pathLine.Points.Length)
                    _currentPointIndex++;
                else
                    Die();
                _targetPoint = _pathLine.Points[_currentPointIndex];
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (_pathLine && _pathLine.Points.Length != 0)
            foreach (var pathPoint in _pathLine.Points)
            {
                Gizmos.DrawSphere(pathPoint, 0.1f);
            }
    }

    protected override void Fly()
    {
        if (_pathLine != null)
        {
            var deltaSpeed = _speed * Time.fixedDeltaTime;
            _rigidbody.position = Vector2.MoveTowards(transform.position, _targetPoint, deltaSpeed);
        }
        else
            base.Fly();
    }

    public virtual void SetPathLine(PointsPattern pathLine)
    {
        _pathLine = pathLine;
        _targetPoint = _pathLine.Points[_currentPointIndex];
    }

    public override void SetSettingsFromWave(WaveElement waveElement)
    {
        SetPathLine(waveElement.PathLine);
    }
}

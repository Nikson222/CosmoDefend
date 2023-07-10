using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PointsPattern : MonoBehaviour
{
    [SerializeField] Vector2[] _Points;
    public Vector2[] Points { get { return _Points; } }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (_Points.Length != 0)
            foreach (var pathPoint in _Points)
            {
                Gizmos.DrawSphere(pathPoint, 0.1f);
            }
    }
}

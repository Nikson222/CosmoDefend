using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Levels
{
    [CreateAssetMenu(menuName = "Level Config", order = 2, fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private bool _isLooped;
        [SerializeField] private Wave[] waves;

        public Wave[] Waves { get { return waves; } }
        public bool IsLooped { get { return _isLooped; } }
    }

    [Serializable]
    public class Wave
    {
        [SerializeField] private bool _isSimultaneouslySpawn;
        [SerializeField] private string name = "Wave";
        [SerializeField] private WaveElement[] _waveElements;
        [SerializeField] private float _waveDuration;
        public WaveElement[] WaveElements { get => _waveElements; }
        public bool IsSimultaneouslySpawn { get => _isSimultaneouslySpawn; }
        public float WaveDuration { get => _waveDuration; }
    }

    [Serializable]
    public class WaveElement
    {
        [SerializeField] private string name = "WaveElement";
        [SerializeField] private EnemySpacecraft _enemyPrefab;
        [SerializeField] private int _count;
        [SerializeField] private bool _isRandomizeDuration;
        [SerializeField] private float _minDuration;
        [SerializeField] private float _maxDuration;
        [SerializeField] private float _switchDuration;

        [Space]
        [SerializeField] private bool _isPlayerTarget;
        [SerializeField] private Transform _target;
        [SerializeField] private PointsPattern _pathLine;

        [Space]
        [SerializeField] private Vector2 _spawnPoint;
        [SerializeField] private float _xRangeSpawn;
        [SerializeField] private float _yRangeSpawn;

        public float MinDuration { get { return _minDuration; } }
        public float MaxDuration { get { return _maxDuration; } }
        public float SwitchDuration { get { return _switchDuration; } }
        public bool IsRandomizeDuratiom { get { return _isRandomizeDuration; } }
        public bool IsPlayerTarget { get { return _isPlayerTarget; } }
        public int Count { get { return _count; } }
        public EnemySpacecraft EnemySpacecraft { get { return _enemyPrefab; } }

        public Transform Target { get { return _target; } }
        public PointsPattern PathLine { get { return _pathLine;} }
        public Vector2 SpawnPoint { get { return _spawnPoint; } }
        public float XRangeSpawn { get { return _xRangeSpawn; } }
        public float YRangeSpawn { get { return _yRangeSpawn; } }

        public void SetTargetSettingForWave(Transform target)
        {
            _target = target;
        }
    }
}
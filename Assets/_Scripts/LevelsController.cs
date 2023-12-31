using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class LevelsController : MonoBehaviour
    {
        [SerializeField] private LevelConfig _menuLevelConfig;

        [SerializeField] private LevelConfig[] _levelConfigs;

        private List<int> _allIndexesFlomConfigs;

        private List<int> _indexesOfUnlockededLevels = new List<int>();

        [SerializeField] private EnemySpawner _enemySpawner;

        [SerializeField] private Transform _targetForEnemy;

        [SerializeField] private LevelSelectPanel _selectPanel;


        private Action<int> _onLevelUnlock;
        private Action _onLevelLaunch;

        private void Start()
        {
            UpdateTargetForWave(_menuLevelConfig, _targetForEnemy);

            LaunchLevel(_menuLevelConfig);

            DontDestroyOnLoad(this);

            _onLevelUnlock += _selectPanel.UnlockButton;
            _selectPanel.OnLevelSelect += LaunchLevel;

            _allIndexesFlomConfigs = GetPossibleIndexes();

            _selectPanel.Init(_levelConfigs, _indexesOfUnlockededLevels);

            UnlockLevel(0);
        }

        private void LaunchLevel(int levelIndex)
        {
            _onLevelLaunch?.Invoke();
            _enemySpawner.ClearWasteFromLastLevel();
            if (levelIndex < _levelConfigs.Length)
                _enemySpawner.SpawnLevelWaves(_levelConfigs[levelIndex]);
        }

        private void LaunchLevel(LevelConfig levelConfig)
        {
            _onLevelLaunch?.Invoke();
            _enemySpawner.ClearWasteFromLastLevel();
            _enemySpawner.SpawnLevelWaves(levelConfig);
        }

        public void UpdateTargetForWave(LevelConfig levelConfig, Transform target)
        {
            foreach (var wave in levelConfig.Waves)
                foreach (var element in wave.WaveElements)
                    element.SetTargetSettingForWave(target);
        }

        private List<int> GetPossibleIndexes()
        {
            List<int> indexes = new List<int>();

            int levelIndex = 0;
            foreach (var level in _levelConfigs)
            {
                indexes.Add(levelIndex);
                levelIndex++;
            }

            return indexes;
        }

        private List<int> GetUnlockedIndexes(HashSet<int> unlockedIndexes)
        {
            List<int> indexes = new List<int>();

            foreach (var index in unlockedIndexes)
            {
                indexes.Add(index);
            }

            return indexes;
        }

        public void UnlockLevel(int index)
        {
            if (_allIndexesFlomConfigs.Contains(index))
                _indexesOfUnlockededLevels.Add(index);

            _onLevelUnlock?.Invoke(index);
        }
    }
}

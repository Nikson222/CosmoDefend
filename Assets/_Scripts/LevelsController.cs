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

        private LevelSelectPanel _selectPanel;
        public LevelConfig[] LevelConfigs => _levelConfigs;

        public LevelSelectPanel SelectPanel
        {
            get => _selectPanel;
            set => _selectPanel = value;
        }

        public LevelConfig MenuLevelConfig => _menuLevelConfig;

        private Action<int> _onLevelUnlock;
        private Action _onLevelLaunch;

        private ScenesController _sceneManager;

        public void Init(ScenesController sceneManager)
        {
            _sceneManager = sceneManager;
            _sceneManager.OnTransitionInEnd += _enemySpawner.ClearWasteFromLastLevel;

            _sceneManager.OnTransitionInEnd += () =>
            {
                if (_sceneManager.IsMainMenuScene)
                {
                    _selectPanel.Init(_levelConfigs, _indexesOfUnlockededLevels);

                    _selectPanel.OnLevelSelect += (LevelConfig levelConfig) => { StartCoroutine(LaunchLevelWithTransitionRoutine(levelConfig)); };
                }
            };

            _selectPanel.Init(_levelConfigs, _indexesOfUnlockededLevels);
            _selectPanel.OnLevelSelect += (LevelConfig levelConfig) => { StartCoroutine(LaunchLevelWithTransitionRoutine(levelConfig)); };

            _allIndexesFlomConfigs = GetPossibleIndexes();

            //_selectPanel.Init(_levelConfigs, _indexesOfUnlockededLevels);

            UnlockLevel(0);
        }

        public IEnumerator LaunchLevelWithTransitionRoutine(LevelConfig levelConfig)
        {
            _onLevelLaunch?.Invoke();
            yield return _sceneManager.LoadLevelScene();

            _enemySpawner.SpawnLevelWaves(levelConfig);
        }

        public void LaunchLevel(LevelConfig levelConfig)
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

            if (_selectPanel)
                _selectPanel.UnlockButton(index);
        }

        public void StartMenuLevelConfig()
        {
            LaunchLevel(_menuLevelConfig);
        }
    }
}

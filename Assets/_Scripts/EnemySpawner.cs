using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Levels;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector2 _defaultSpawnPosition;
    [SerializeField] private Vector2 _defaultSpawnRange;

    [SerializeField] protected EnemySpacecraft[] _possibleEnemysInLevels;

    private HashSet<EnemySpacecraft> _possibleEnemys = new HashSet<EnemySpacecraft>();

    private Dictionary<EnemySpacecraft, ObjectPooler<EnemySpacecraft>> _prefabsPooler = new Dictionary<EnemySpacecraft, ObjectPooler<EnemySpacecraft>>();

    public Dictionary<EnemySpacecraft, ObjectPooler<EnemySpacecraft>> Enemiespoolers { get => _prefabsPooler; }


    private Coroutine spawnCoroutine;

    private Action _onClearLastWave;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_defaultSpawnPosition, 0.2f);
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Awake()
    {

        foreach (var item in _possibleEnemysInLevels)
        {
            if (item != null)
                _possibleEnemys.Add(item);
        }

        foreach (var item in _possibleEnemys)
        {
            var objectPooler = new ObjectPooler<EnemySpacecraft>();


            _prefabsPooler.Add(item, objectPooler);
            objectPooler.Init(item, transform);
        }
    }

    public void SpawnLevelWaves(LevelConfig levelConfig)
    {
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
        spawnCoroutine = StartCoroutine(SpawnWavesRoutine(levelConfig));
    }

    private IEnumerator SpawnWavesRoutine(LevelConfig levelConfig)
    {
        foreach (var Wave in levelConfig.Waves)
        {
            for (int i = 0; i < Wave.WaveElements.Length; i++)
            {
                if (!Wave.IsSimultaneouslySpawn && _possibleEnemys.Contains(Wave.WaveElements[i].EnemySpacecraft))
                {
                    var coroutine = StartCoroutine(SpawnElementWithDurationRoutine(Wave.WaveElements[i]));
                    _onClearLastWave += () => { StopCoroutine(coroutine); };

                    yield return new WaitForSeconds(Wave.WaveElements[i].SwitchDuration);
                    yield return coroutine;
                }
                else
                {
                    Coroutine coroutine = null;
                    foreach (var waveElement in Wave.WaveElements)
                    {
                        coroutine = StartCoroutine(SpawnElementWithDurationRoutine(waveElement));
                        _onClearLastWave += () => { StopCoroutine(coroutine); };

                        yield return new WaitForSeconds(waveElement.SwitchDuration);
                    }
                    yield return coroutine;
                }
            }

            yield return new WaitForSeconds(Wave.WaveDuration);
        }

        if (levelConfig.IsLooped)
            SpawnLevelWaves(levelConfig);
    }

    private IEnumerator SpawnElementWithDurationRoutine(WaveElement waveElement)
    {
        if (_possibleEnemys.Contains(waveElement.EnemySpacecraft))
        {
            for (int i = 0; i < waveElement.Count; i++)
            {
                var enemy = _prefabsPooler[waveElement.EnemySpacecraft].GetObject();

                if (waveElement.SpawnPoint != Vector2.zero)
                {
                    float xOffset = UnityEngine.Random.Range(-waveElement.XRangeSpawn, waveElement.XRangeSpawn);
                    float YOffset = UnityEngine.Random.Range(-waveElement.YRangeSpawn, waveElement.YRangeSpawn);
                    Vector2 spawnPoint = new Vector2(waveElement.SpawnPoint.x + xOffset, waveElement.SpawnPoint.y + YOffset);

                    SetSpawnPosition(enemy.transform, spawnPoint);
                }
                else
                    SetSpawnPosition(enemy.transform);

                enemy.SetSettingsFromWave(waveElement);

                float duration;
                if (waveElement.IsRandomizeDuratiom)
                    duration = UnityEngine.Random.Range(waveElement.MinDuration, waveElement.MaxDuration);
                else
                    duration = waveElement.SwitchDuration;

                print(duration);
                yield return new WaitForSeconds(duration);
            }
        }
    }

    private void SpawnElementWithOutDuration(WaveElement waveElement)
    {
        if (_possibleEnemys.Contains(waveElement.EnemySpacecraft))
        {
            for (int i = 0; i < waveElement.Count; i++)
            {
                var enemy = _prefabsPooler[waveElement.EnemySpacecraft].GetObject();
                HealthBarsController.Instance.CreateHealthBar(enemy);

                if (waveElement.SpawnPoint != Vector2.zero)
                {
                    float xOffset = UnityEngine.Random.Range(-waveElement.XRangeSpawn, waveElement.XRangeSpawn);
                    float YOffset = UnityEngine.Random.Range(-waveElement.YRangeSpawn, waveElement.YRangeSpawn);
                    Vector2 spawnPoint = new Vector2(waveElement.SpawnPoint.x + xOffset, waveElement.SpawnPoint.y + YOffset);

                    SetSpawnPosition(enemy.transform, spawnPoint);
                }

                else
                    SetSpawnPosition(enemy.transform);

                enemy.SetSettingsFromWave(waveElement);
            }
        }
    }

    public void SetSpawnPosition(Transform enemy)
    {
        var spawnPos = new Vector2(UnityEngine.Random.Range(-_defaultSpawnRange.x, _defaultSpawnRange.x) + _defaultSpawnPosition.x,
             UnityEngine.Random.Range(-_defaultSpawnRange.y, _defaultSpawnRange.y) + _defaultSpawnPosition.y);

        enemy.position = spawnPos;
    }

    public void SetSpawnPosition(Transform enemy, Vector2 position)
    {
        enemy.position = position;
    }

    public void ClearWasteFromLastLevel()
    {
        _onClearLastWave?.Invoke();
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
        print("Cleared");
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}

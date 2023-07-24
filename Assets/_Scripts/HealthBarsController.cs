using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBarsController : MonoBehaviour
{
    public static HealthBarsController Instance;

    private ObjectPooler<HealthBar> _pooler = new ObjectPooler<HealthBar>();

    [SerializeField] private SpaceCraft _playerSpaceCraft;
    [SerializeField] private GameObject _healthBarPrefab;
    [SerializeField] private Canvas _canvas;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

        _pooler.Init(_healthBarPrefab.GetComponent<HealthBar>(), _canvas.transform);

        _pooler.GetObject().Init(_playerSpaceCraft);
    }

    public void CreateHealthBar(EnemySpacecraft enemySpacecraft)
    {
        _pooler.GetObject().Init(enemySpacecraft);
    }
}

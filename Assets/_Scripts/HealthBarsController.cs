using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarsController : MonoBehaviour
{
    public static HealthBarsController Instance;

    private ObjectPooler<HealthBar> _pooler = new ObjectPooler<HealthBar>();

    [SerializeField] private GameObject _healthBarPrefab;
    [SerializeField] private Canvas _canvas;

    private void Start()
    {
        DontDestroyOnLoad(_canvas);

        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        _pooler.Init(_healthBarPrefab.GetComponent<HealthBar>(), _canvas.transform);
    }

    public void CreateHealthBar(EnemySpacecraft enemySpacecraft)
    {
        if(_canvas.worldCamera == null)
            _canvas.worldCamera = Camera.main;
        _pooler.GetObject().Init(enemySpacecraft);
    }
}

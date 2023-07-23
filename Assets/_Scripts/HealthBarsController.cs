using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBarsController : MonoBehaviour
{
    public static HealthBarsController Instance;

    private ObjectPooler<HealthBar> _pooler = new ObjectPooler<HealthBar>();

    [SerializeField] private GameObject _healthBarPrefab;
    [SerializeField] private Canvas _canvas;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(_canvas);

        _pooler.Init(_healthBarPrefab.GetComponent<HealthBar>(), _canvas.transform);

        SceneManager.activeSceneChanged += (Scene scene, Scene scene1) => { _canvas.worldCamera = Camera.main; };
    }

    public void CreateHealthBar(EnemySpacecraft enemySpacecraft)
    {
        _pooler.GetObject().Init(enemySpacecraft);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Levels;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private LevelsController _levelController;

    [SerializeField] private ScenesController _scenesController;

    public LevelsController LevelController => _levelController;
    public ScenesController ScenesController => _scenesController;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(gameObject);

        StartCoroutine(InitRoutine());
    }

    private IEnumerator InitRoutine()
    {

        var loadRoutine = _scenesController.LoadMenuScene();
        yield return loadRoutine;

        _levelController.Init(_scenesController);

        _levelController.StartMenuLevelConfig();
    }

    public void SetMenuTarget(Transform targetTransform)
    {
        _levelController.UpdateTargetForWave(_levelController.MenuLevelConfig, targetTransform);
    }
}

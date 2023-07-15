using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Levels;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private SpaceCraft _player;

    [SerializeField] private LevelsController _levelController;

    [SerializeField] private ScenesController _scenesController;

    [SerializeField] private PanelsRecorder _panelRecorder;

    private void Start()
    {
        SceneManager.activeSceneChanged += ApplySettingToScene;

        _levelController.Init(_scenesController);

        if(Instance == null)
            Instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this);

        Application.targetFrameRate = 60;
        _player.OnDie += () => { _scenesController.LoadLevelScene(); };
    }

    private IEnumerator RestartGameRoutine()
    {
        yield return new WaitForSeconds(3f);
        var scene = SceneManager.GetActiveScene();
        StopAllCoroutines();
        SceneManager.LoadScene(scene.buildIndex, LoadSceneMode.Single);
    }

    private void ApplySettingToScene(Scene scene, Scene scene1)
    {
        if (_scenesController.IsMainMenuScene)
        {
            _levelController.StartMenuLevelConfig();
            _player.gameObject.SetActive(false);
        }
        else if (_scenesController.IsLevelScene)
        {
            _player.gameObject.SetActive(true);
        }
    }
}

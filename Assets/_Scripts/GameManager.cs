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

    private void Start()
    {
        _levelController.Init();

        if (_scenesController.IsMainMenuScene)
            _levelController.StartMenuLevelConfig();

        if(Instance == null)
            Instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this);

        Application.targetFrameRate = 60;
        //_player.OnDie += () => { StartCoroutine(RestartGameRoutine()); };
    }

    private IEnumerator RestartGameRoutine()
    {
        yield return new WaitForSeconds(3f);
        var scene = SceneManager.GetActiveScene();
        StopAllCoroutines();
        SceneManager.LoadScene(scene.buildIndex, LoadSceneMode.Single);
    }

    private void SpawnPlayer()
    {

    }

}

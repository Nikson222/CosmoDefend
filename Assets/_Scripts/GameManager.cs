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

    [SerializeField] private LevelsController _controller;
    public LevelsController Controller => _controller;

    private void Start()
    {
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

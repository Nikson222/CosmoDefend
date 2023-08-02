using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Levels;

public class ScenesController : MonoBehaviour
{
    public Action OnTransitionInEnd;

    [SerializeField] private TransitionHandler _transitionHandler;

    [SerializeField] private AudioClip _menuMusic, _levelMusic;
    public bool IsMainMenuScene { get => SceneManager.GetActiveScene().buildIndex.Equals(1); }
    public bool IsLevelScene { get => SceneManager.GetActiveScene().buildIndex.Equals(2); }


    public Coroutine LoadMenuScene()
    {
        AudioManager.Instance.PlayMusic(_menuMusic);
        return StartCoroutine(LoadTransitionRoutine(1));
    }

    public Coroutine LoadLevelScene()
    {
        AudioManager.Instance.PlayMusic(_levelMusic);
        return StartCoroutine(LoadTransitionRoutine(2));
    }

    private IEnumerator LoadTransitionRoutine(int buildIndex)
    {
        var TransitionInRoutine = _transitionHandler.PlayTransitionIn();

        yield return TransitionInRoutine;


        var sceneLoadingOperation = SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Single);

        yield return sceneLoadingOperation;

        OnTransitionInEnd?.Invoke();

        var TransitionOutRoutine = _transitionHandler.PlayTransitionOut();

        yield return TransitionOutRoutine;
    }
}

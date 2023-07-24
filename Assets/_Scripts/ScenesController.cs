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

    public bool IsMainMenuScene { get => SceneManager.GetActiveScene().buildIndex.Equals(0); }
    public bool IsLevelScene { get => SceneManager.GetActiveScene().buildIndex.Equals(1); }


    public Coroutine LoadMenuScene()
    {
        return StartCoroutine(LoadTransitionRoutine(0));
    }

    public Coroutine LoadLevelScene()
    {
        return StartCoroutine(LoadTransitionRoutine(1));
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

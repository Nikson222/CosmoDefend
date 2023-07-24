using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionHandler : MonoBehaviour
{
    private const string LOADING_PARAMETR = "IsTransitionIn";

    [SerializeField] Animator _animator;

    private void Start()
    {
        //DontDestroyOnLoad(this);
    }

    public Coroutine PlayTransitionIn()
    {
        _animator.SetBool(LOADING_PARAMETR, true);
        return StartCoroutine(TransitionAnimationRoutine(_animator.GetCurrentAnimatorClipInfo(0).Length));   
    }

    public Coroutine PlayTransitionOut()
    {
        _animator.SetBool(LOADING_PARAMETR, false);

        return StartCoroutine(TransitionAnimationRoutine(_animator.GetCurrentAnimatorClipInfo(0).Length));
    }

    private IEnumerator TransitionAnimationRoutine(float length)
    {
        yield return new WaitForSeconds(length);
    }
}

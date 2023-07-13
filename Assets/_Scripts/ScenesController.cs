using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    [SerializeField] private string _mainMenuSceneName;

    public Action onMainMenuSceneEnable;

    public bool IsMainMenuScene { get => SceneManager.GetActiveScene().name.Equals(_mainMenuSceneName); }

}

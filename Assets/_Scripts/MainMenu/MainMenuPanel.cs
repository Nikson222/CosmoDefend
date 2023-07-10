using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : Panel
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _vibrationButton;

    protected override void Awake()
    {
        base.Awake();
        _playButton.onClick.AddListener(OpenLevelsPanel);
        _exitButton.onClick.AddListener(ExitGame);
    }

    private void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void OpenLevelsPanel()
    {
        _panelSwitcher.SwitchToWindow("LevelSelect");
    }
}

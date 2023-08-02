using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanel : Panel
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _vibrationButton;

    private bool _isMusicOff = false, _isSoundOff = false, _isVibrationOff = false;
    private string _isMusicOffSaveNave = "_isMusicOff", _isSoundOffSaveNave = "_isSoundOff", _isVibrationOffSaveNave = "_isVibrationOff";

    protected override void Awake()
    {
        base.Awake();
        _playButton.onClick.AddListener(OpenLevelsPanel);
        _exitButton.onClick.AddListener(ExitGame);

        _musicButton.onClick.AddListener(ChangeMusicToogleSprite);
        _soundButton.onClick.AddListener(ChangeSoundToogleSprite);
        _vibrationButton.onClick.AddListener(ChangeVibrationToogleSprite);

        LoadSpritesColor();
    }

    private void LoadSpritesColor()
    {
        _isMusicOff = Convert.ToBoolean(PlayerPrefs.GetInt(_isMusicOffSaveNave));
        _isSoundOff = Convert.ToBoolean(PlayerPrefs.GetInt(_isSoundOffSaveNave));
        _isVibrationOff = Convert.ToBoolean(PlayerPrefs.GetInt(_isVibrationOffSaveNave));

        if (_isMusicOff)
        {
            _isMusicOff = !_isMusicOff;
            ChangeMusicToogleSprite();
        }

        if (_isSoundOff)
        {
            _isSoundOff = !_isSoundOff;
            ChangeSoundToogleSprite();
        }

        if (_isVibrationOff)
        {
            _isVibrationOff = !_isVibrationOff;
            ChangeVibrationToogleSprite();
        }
    }

    private void SaveSpritesColor()
    {
        PlayerPrefs.SetInt(_isMusicOffSaveNave, Convert.ToInt32(_isMusicOff));
        PlayerPrefs.SetInt(_isSoundOffSaveNave, Convert.ToInt32(_isSoundOff));
        PlayerPrefs.SetInt(_isVibrationOffSaveNave, Convert.ToInt32(_isVibrationOff));
    }

    private void ExitGame()
    {
        SaveSpritesColor();

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

    private void ChangeMusicToogleSprite()
    {
        _isMusicOff = !_isMusicOff;
        if (_isMusicOff)
            _musicButton.image.color = Color.grey;
        else
            _musicButton.image.color = Color.white;
    }

    private void ChangeSoundToogleSprite()
    {
        _isSoundOff = !_isSoundOff;
        if (_isSoundOff)
            _soundButton.image.color = Color.grey;
        else
            _soundButton.image.color = Color.white;
    }

    private void ChangeVibrationToogleSprite()
    {
        _isVibrationOff = !_isVibrationOff;
        GameManager.Instance.IsCanVibrate = !_isVibrationOff;

        if (_isVibrationOff)
            _vibrationButton.image.color = Color.grey;
        else
            _vibrationButton.image.color = Color.white;
    }

    private void OnDestroy()
    {
        SaveSpritesColor();
    }
}

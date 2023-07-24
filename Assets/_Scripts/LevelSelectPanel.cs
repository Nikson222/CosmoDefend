using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Levels;
using UnityEngine.SceneManagement;

public class LevelSelectPanel : Panel
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private GameObject _levelButtonPrefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private ScrollRect _scrollRect;

    public Action<LevelConfig> OnLevelSelect;
    private Dictionary<int, LevelButton> _levelButtons = new Dictionary<int, LevelButton>();

    private void Awake()
    {  
        _exitButton.onClick.AddListener(() => { _panelSwitcher.SwitchToWindow("MainMenu"); });

        if(GameManager.Instance != null)
            GameManager.Instance.LevelController.SelectPanel = this;
    }

    public void Init(LevelConfig[] levelConfigs, List<int> availablesLevels)
    {
        int levelIndex = 0;

        foreach (var level in levelConfigs)
        {
            var button = Instantiate(_levelButtonPrefab, _parent.position, Quaternion.identity).GetComponent<LevelButton>();

            _levelButtons.Add(levelIndex, button);

            button.transform.SetParent(_parent);
            button.transform.SetAsFirstSibling();
            button.transform.localScale = Vector3.one;

            button.AddListener(() => { SelectLevel(level); });

            button.Init((levelIndex+1).ToString(), availablesLevels.Contains(levelIndex));

            levelIndex++;
        }

        _scrollRect.horizontalNormalizedPosition = 0;
    }
    
    private void SelectLevel(LevelConfig levelConfig)
    {
        OnLevelSelect?.Invoke(levelConfig);
    }

    public void UnlockButton(int index)
    {
        _levelButtons[index].UnlockButton();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    private PanelSwitcher _instance;
    public PanelSwitcher Instance => _instance;

    [SerializeField] private PanelsRecorder panelsRecorder;

    private Panel _currentPanel;

    [SerializeField] private Panel _defaultPanel;
    
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);

        _currentPanel = _defaultPanel;
    }

    private void Start()
    {
        foreach (var item in panelsRecorder.Panels.Values)
        {
            item.InjectPanelSwitcher(this);
            item.Hide();
        }

        _defaultPanel.Show();
    }


    public void SwitchToWindow(string panelID)
    {
        Panel nextWindow = panelsRecorder.GetWindow(panelID);

        if (nextWindow != null)
        {
            _currentPanel.Hide();
            _currentPanel = nextWindow;
            _currentPanel.Show();
        }
        else
        {
            Debug.LogWarning($"Window of ID {panelID} is not registered.");
        }
    }
}

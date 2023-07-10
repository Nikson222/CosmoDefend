using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Panel : MonoBehaviour
{
    [SerializeField] private string _panelID;
    public string panelID => _panelID;

    public Action OnSwitchPanel;

    protected PanelSwitcher _panelSwitcher;

    protected virtual void Awake()
    {
    }

    public void InjectPanelSwitcher(PanelSwitcher panelSwitcher)
    {
        _panelSwitcher = panelSwitcher;
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
}

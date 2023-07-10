using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsRecorder : MonoBehaviour
{
    private static PanelsRecorder _instance;
    public static PanelsRecorder Instance => _instance;
    [SerializeField] private List<Panel> _avaliablePanels;

    private Dictionary<string, Panel> _panels = new Dictionary<string, Panel>();
    public Dictionary<string, Panel> Panels => _panels;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        foreach (Panel panel in _avaliablePanels)
        {
            RegisterWindow(panel);
        }
    }

    public void RegisterWindow(Panel panel)
    {
        if (!_panels.ContainsKey(panel.panelID))
        {
            _panels.Add(panel.panelID, panel);
        }
        else
        {
            Debug.LogWarning($"Window of ID {panel.panelID} is already registered.");
        }
    }

    public Panel GetWindow(string panelID)
    {
        if (_panels.ContainsKey(panelID))
        {
            return _panels[panelID];
        }
        else
        {
            Debug.LogWarning($"Window of ID {panelID} is not registered.");
            return null;
        }
    }
}

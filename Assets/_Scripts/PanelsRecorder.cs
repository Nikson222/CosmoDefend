using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelsRecorder : MonoBehaviour
{
    private static PanelsRecorder _instance;
    public static PanelsRecorder Instance => _instance;
    [SerializeField] private List<Panel> _avaliablePanels;
    private Dictionary<string, Panel> _panels = new Dictionary<string, Panel>();

    public List<Panel> AvaliablePanels => _avaliablePanels;
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

        SceneManager.activeSceneChanged += GetPanelsFromNewScene;
    }

    public void RegisterWindow(Panel panel)
    {
        if (!_panels.ContainsKey(panel.panelID))
        {
            _panels.Add(panel.panelID, panel);
            _avaliablePanels.Add(panel);
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

    private void GetPanelsFromNewScene(Scene scene, Scene scene1)
    {
        var panels = FindObjectsOfType<Panel>();

        _avaliablePanels.Clear();
        _panels.Clear();

        foreach (var panel in panels)
        {
            RegisterWindow(panel);
        }
    }
}

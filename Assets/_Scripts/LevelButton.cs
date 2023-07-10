using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Levels;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Text _text;
    [SerializeField] private Image _lockImage;

    public void AddListener(UnityAction call)
    {
        _button.onClick.AddListener(call);
    }

    public void UnlockButton()
    {
        _button.interactable = true;
        _lockImage.gameObject.SetActive(false);
    }

    public void LockButton()
    {
        _button.interactable = false;
        _lockImage.gameObject.SetActive(true);
    }

    public void Init(string text, bool interactable)
    {
        _text.text = text;
        _button.interactable = interactable;

        if (interactable)
            UnlockButton();
        else
            LockButton();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private AudioClip _clickSound;

    private void Start()
    {
        _button.onClick.AddListener(() => { AudioManager.Instance.PlaySfx(_clickSound); });
    }
}

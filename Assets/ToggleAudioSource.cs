using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAudioSource : MonoBehaviour
{
    [SerializeField] private bool _isToggleMusic, _isToggleSFX;

    public void ToogleAudio()
    {
        if(_isToggleMusic)
            AudioManager.Instance.ToggleMusic();
        if(_isToggleSFX)
            AudioManager.Instance.ToggleSfx();
    }
}

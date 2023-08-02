using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _musicSource, _sfxSourse;
    private string _isMusicOffSaveNave = "_isMusicOff", _isSoundOffSaveNave = "_isSoundOff";

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        _musicSource.mute = Convert.ToBoolean(PlayerPrefs.GetInt(_isMusicOffSaveNave));
        _sfxSourse.mute = Convert.ToBoolean(PlayerPrefs.GetInt(_isMusicOffSaveNave));
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (musicClip == null)
        {
            return;
        }

        _musicSource.clip = musicClip;
        _musicSource.Play();
    }

    public void PlaySfx(AudioClip sfxClip)
    {
        if (sfxClip == null)
        {
            return;
        }

        _sfxSourse.PlayOneShot(sfxClip);
    }

    public void ToggleSfx()
    {
        _sfxSourse.mute = !_sfxSourse.mute;
    }

    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }
}

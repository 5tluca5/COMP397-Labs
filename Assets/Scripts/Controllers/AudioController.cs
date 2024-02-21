using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : PersistentSingleton<AudioController>
{
    [SerializeField] AudioSource sfxPlayer;
    [SerializeField] AudioSource bgmPlayer;

    public void PlaySfx(AudioAsset asset)
    {
        sfxPlayer.PlayOneShot(asset.AudioFile);
    }

    public void PlayBgm(AudioAsset asset) 
    {
        bgmPlayer.loop = asset.IsLooping;
        bgmPlayer.clip = asset.AudioFile;
        bgmPlayer.Play();
    }
}

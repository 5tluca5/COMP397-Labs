using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class AudioData
{
    public string audioName;
    public AudioClip audioClip;
}

/// <summary>
/// Audio Manager is scene dependent. Which means that it will control the audios for a specific scene.
/// </summary>

public class AudioManager : MonoBehaviour, IObserver<PlayerEnum>
{
    [SerializeField] private List<AudioAsset> audios = new List<AudioAsset>();
    [SerializeField] private Subject<PlayerEnum> playerSubject;
    [SerializeField] string musicName;
    private void Awake()
    {
        var go = GameObject.FindGameObjectWithTag("Player");

        if(go != null)
            playerSubject = GameObject.FindGameObjectWithTag("Player").GetComponent<Subject<PlayerEnum>>();
    }

    private void Start()
    {
        var music = audios.Find(x => x.AudioName == musicName);
        AudioController.Instance.PlayBgm(music);
    }
    private void OnEnable()
    {
        if(playerSubject)
            playerSubject.AddObserver(this);
    }

    private void OnDisable()
    {
        if (playerSubject)
            playerSubject.RemoveObserver(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNotify(PlayerEnum enums)
    {
        AudioAsset asset = null;

        switch(enums)
        {
            case PlayerEnum.Die:
                asset = audios.Find(x => x.AudioName == "Die");
                
                break;
            case PlayerEnum.Jump:
                asset = audios.Find(x => x.AudioName == "Jump");
                break;
        }

        AudioController.Instance.PlaySfx(asset);
    }

}

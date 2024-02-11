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

public class AudioManager : MonoBehaviour, IObserver<PlayerEnum>
{
    [SerializeField] private List<AudioData> audios = new List<AudioData>();
    [SerializeField] AudioSource sfxPlayer;
    [SerializeField] private Subject<PlayerEnum> playerSubject;

    private void Awake()
    {
        playerSubject = GameObject.FindGameObjectWithTag("Player").GetComponent<Subject<PlayerEnum>>();

    }
    private void OnEnable()
    {
        playerSubject.AddObserver(this);
    }

    private void OnDisable()
    {
        playerSubject.RemoveObserver(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNotify(PlayerEnum enums)
    {
        switch(enums)
        {
            case PlayerEnum.Die:
                sfxPlayer.PlayOneShot(audios.Find(x => x.audioName == "Die").audioClip);
                break;
            case PlayerEnum.Jump:
                sfxPlayer.PlayOneShot(audios.Find(x => x.audioName == "Jump").audioClip);
                break;
        }
    }

    public void PlayerSfx(string audioName)
    {
        if(audios.Exists(x => audioName == x.audioName))
        {
            sfxPlayer.PlayOneShot(audios.Find(x => x.audioName == audioName).audioClip);
        }
    }
}

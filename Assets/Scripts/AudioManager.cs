using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum bgmenum
{
    GJP23_BerryFlavored_draft1,
    GJP23_GentleWarmth,
    GJP23_SomethingToLookForwardTo_drft1,
}

public enum sfxenum
{
    Male1_AllGood,
    Male1_Alright,
    Male1_Damn_defeated,
    Male1_DoesThisWorkForYou_questioning,
    Male1_Hello,
    Male1_Hm1,
    Male1_Hm2,
    Male1_HowsThis_questioning,
    Male1_Huh1,
    Male1_Huh2,
    Male1_MakesSense,
    Male1_NoProblem,
    Male1_OhComeOn,
    Male1_Seriously,
    Male1_Taho1,
    Male1_Taho2,
    Male1_ThisDoesntLookTooGood_concerned,
    Male1_VeryWell,
    Male1_WHATDOYOUMEAN_angry,
    Male1_WhatDoYouMean_question,
    Male1_WhatsUp,
    Male1_YouKnowWhatImSaying_question
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource soundEffects;

    private List<AudioClip> backgroundMusicList = new List<AudioClip>();
    private List<AudioClip> sfxMusicList = new List<AudioClip>();

    public AudioSource BackgroundMusic { get { return backgroundMusic; } set { backgroundMusic = value; } }
    public AudioSource SoundEffects { get { return soundEffects; } set { soundEffects = value; } }

    // Start is called before the first frame update
    void Start()
    {
        foreach (string audio in System.Enum.GetNames(typeof(bgmenum)))
            backgroundMusicList.Add(Resources.Load<AudioClip>("bgm/" + audio));

        foreach (string audio in System.Enum.GetNames(typeof(sfxenum)))
            sfxMusicList.Add(Resources.Load<AudioClip>("sfx/" + audio));

        backgroundMusic.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBackgroundMusic(bgmenum audio)
    {
        backgroundMusic.clip = backgroundMusicList[(int)audio];
        backgroundMusic.Play();
    }

    public void StopBackgroundMusicSound()
    {
        backgroundMusic.Stop();
    }
}

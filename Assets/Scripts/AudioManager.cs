using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum bgmenum
{
    GJP23_BerryFlavored_draft1,
    GJP23_GentleWarmth,
    GJP23_SomethingToLookForwardTo_drft1,
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource soundEffects;

    [SerializeField] private List<AudioClip> backgroundMusicList;

    public AudioSource BackgroundMusic { get { return backgroundMusic; } set { backgroundMusic = value; } }
    public AudioSource SoundEffects { get { return soundEffects; } set { soundEffects = value; } }

    // Start is called before the first frame update
    void Start()
    {
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

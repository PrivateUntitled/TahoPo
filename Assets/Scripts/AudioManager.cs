using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum bgmenum
{
    GJP23_BerryFlavored_final,
    GJP23_GentleWarmth_final,
    GJP23_SlowAndSteady,
    GJP23_SomethingToLookForwardTo_final,
}

public enum sfxenum
{
    child1_a_what_now_cursed,
    child1_ee_yup_cursed,
    child1_huh_cursed,
    child1_i_totally_get_it_cursed,
    child1_nuh_uh_cursed,
    child1_oh_my_cursed,
    female1_heeeey_beckon,
    female1_hey,
    female1_hm_thinking,
    female1_huh,
    female1_huh_dead,
    female1_I_understand_cheerful,
    female1_mm_hm_cheerful,
    female1_mm_hm_cheerful_01,
    female1_oh_questioning,
    female1_whats_up,
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
    Male1_YouKnowWhatImSaying_question,
    sfx_birds1,
    sfx_birds2,
    sfx_birds3,
    sfx_birds4,
    sfx_bottleliquidPour1,
    sfx_bottleliquidPour2,
    sfx_buttonClick1,
    sfx_customerLeaves1,
    sfx_customerLeaves2,
    sfx_customerLeaves3,
    sfx_customerLeaves4,
    sfx_customerSuccessful1,
    sfx_guessCorrect,
    sfx_guessWrong,
    sfx_leaves1,
    sfx_leaves2,
    sfx_leaves3,
    sfx_leaves4,
    sfx_longAmbience_windAndLeaves,
    sfx_plasticCrinkle1,
    sfx_plasticCrinkle2,
    sfx_plasticCupFall,
    sfx_pouringPearls,
    sfx_scoopMetallic1,
    sfx_scoopMetallic2,
    sfx_scoopPlastic1,
    sfx_taho1,
    sfx_taho2,
    TanodHmm,
    TanodHmm2,
    TanodHuh,
    TanodHuh2
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource soundEffects;
    [SerializeField] private AudioSource ambienceSource;

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

    public void PlayRandomSFX(List<sfxenum> sfxList)
    {
        sfxenum audio = sfxList[Random.Range(0, sfxList.Count)];
        soundEffects.PlayOneShot(sfxMusicList[(int)audio]);
    }

    public void StopBackgroundMusicSound()
    {
        backgroundMusic.Stop();
    }

    public void PlaySFX(sfxenum audio)
    {
        soundEffects.PlayOneShot(sfxMusicList[(int)audio]);
    }

    public void PlayAmbience(List<sfxenum> sfxList)
    {
        sfxenum audio = sfxList[Random.Range(0, sfxList.Count)];
        ambienceSource.PlayOneShot(sfxMusicList[(int)audio]);
    }
}

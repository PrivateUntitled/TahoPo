using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingMenu;

    [SerializeField] private Slider backgroundMusicSlider;
    [SerializeField] private Slider soundEffectSlider;

    public void StartGame()
    {
        GameManager.instance.LoadMainGame();
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingMenu.SetActive(true);

        backgroundMusicSlider.value = AudioManager.instance.BackgroundMusic.volume;
        soundEffectSlider.value = AudioManager.instance.SoundEffects.volume;
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        settingMenu.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ApplySettings()
    {
        AudioManager.instance.BackgroundMusic.volume = backgroundMusicSlider.value;
        AudioManager.instance.SoundEffects.volume = soundEffectSlider.value;
    }
}

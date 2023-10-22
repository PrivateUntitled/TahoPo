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
        AudioManager.instance.PlaySFX(sfxenum.sfx_buttonClick1);
        GameManager.instance.LoadMainGame();
    }

    public void OpenSettings()
    {
        AudioManager.instance.PlaySFX(sfxenum.sfx_buttonClick1);
        mainMenu.SetActive(false);
        settingMenu.SetActive(true);

        backgroundMusicSlider.value = AudioManager.instance.BackgroundMusic.volume;
        soundEffectSlider.value = AudioManager.instance.SoundEffects.volume;
    }

    public void BackToMainMenu()
    {
        AudioManager.instance.PlaySFX(sfxenum.sfx_buttonClick1);
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
        AudioManager.instance.PlaySFX(sfxenum.sfx_buttonClick1);
        AudioManager.instance.BackgroundMusic.volume = backgroundMusicSlider.value;
        AudioManager.instance.SoundEffects.volume = soundEffectSlider.value;
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, -Vector2.up);

        // If collider hits something
        if (hit.collider == null)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Buttons _button = hit.collider.gameObject.GetComponent<Buttons>();

            if (!_button)
                return;

            if (!_button.IsHovered)
                return;

            switch (_button.ButtonType)
            {
                case ButtonType.PLAY:
                    _button.UnHover();
                    StartGame();
                    break;
                case ButtonType.SETTINGS:
                    _button.UnHover();
                    OpenSettings();
                    break;
                case ButtonType.QUIT:
                    _button.UnHover();
                    QuitGame();
                    break;
                case ButtonType.APPLY:
                    _button.UnHover();
                    ApplySettings();
                    break;
                case ButtonType.BACK:
                    _button.UnHover();
                    BackToMainMenu();
                    break;
                default:
                    break;
            }
        }
    }
}

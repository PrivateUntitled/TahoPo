using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterTextBox;
    [SerializeField] private CutsceneFrames[] cutsceneFrames;
    [SerializeField] private GameObject EndingPanel;
    private int cutsceneIndex;
    private float timePerCharacter = 0.01f;
    private bool textIsDone;

    private void Start()
    {
        cutsceneIndex = 0;
        ShowFrame(cutsceneFrames[cutsceneIndex].frameToShow.name);
        StartCoroutine(typeText(cutsceneFrames[cutsceneIndex].textToDisplay));
    }

    private IEnumerator typeText(string textToDisplay)
    {
        textIsDone = false;
        characterTextBox.maxVisibleCharacters = 0;
        characterTextBox.text = textToDisplay;

        for (int i = 0; i < characterTextBox.text.Length; i++)
        {
            characterTextBox.maxVisibleCharacters++;
            yield return new WaitForSeconds(timePerCharacter);
        }

        textIsDone = true;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && textIsDone)
        {
            cutsceneIndex++;
            if (cutsceneIndex > cutsceneFrames.Length - 1)
            {
                ShowFrame(EndingPanel.name);
            }
            else
            {
                ShowFrame(cutsceneFrames[cutsceneIndex].frameToShow.name);
                StartCoroutine(typeText(cutsceneFrames[cutsceneIndex].textToDisplay));
            }
        }
    }

    public void ShowFrame(string panelToShow)
    {
        for(int i = 0; i < cutsceneFrames.Length; i++)
        {
            Debug.Log(panelToShow + " " + cutsceneFrames[i].frameToShow.name);
            cutsceneFrames[i].frameToShow.SetActive(cutsceneFrames[i].frameToShow.name.Equals(panelToShow));
        }

        EndingPanel.SetActive(EndingPanel.name.Equals(panelToShow));
        characterTextBox.gameObject.transform.parent.gameObject.SetActive(!EndingPanel.name.Equals(panelToShow));
    }

    public void BackToMainMenu()
    {
        Debug.Log("Button Pressed");
        StartCoroutine(GameManager.instance.LoadMainMenuScene2());
    }

    public void QuitGame()
    {
        Debug.Log("Button Pressed");
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    [System.Serializable]
    public struct CutsceneFrames
    {
        public GameObject frameToShow;
        public string textToDisplay;
    }
}

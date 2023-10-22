using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TsismisScene : MonoBehaviour
{
    [SerializeField] private GameObject[] Options;
    [SerializeField] private TextMeshProUGUI questionText;
    private float timePerCharacter = 0.01f;
    private bool textIsDone;

    [SerializeField] private string WrongAnswer;
    [SerializeField] private string NextDay;

    int correct;

    [SerializeField] private TsismisQuestion[] tsismisQuestionaire;

    [SerializeField] private GameObject[] fragments;

    private int questionNumber;

    [System.Serializable]
    public struct TsismisQuestion
    {
        public string Question;
        public AnswerExplanation[] Answers;
        public string correctAnswer;
        public string RightDialogue;
    }

    [System.Serializable]
    public struct AnswerExplanation
    {
        public string answer;
        public string explanation;
        public Sprite chibi;
    }

    private void Start()
    {
        questionNumber = GameManager.instance.CurrentDay;
        correct = GameManager.instance.CurrentDay;
        CheckFragment();
        ShowQuestion();
    }

    public void ShowQuestion()
    {
        StartCoroutine(typeQuestion(tsismisQuestionaire[questionNumber].Question));
    }

    private IEnumerator typeQuestion(string textToDisplay)
    {
        AudioManager.instance.PlayRandomSFX( new List<sfxenum> { sfxenum.Male1_Hm1, sfxenum.Male1_Hm2 } );

        questionText.maxVisibleCharacters = 0;
        questionText.text = textToDisplay;

        for (int i = 0; i < questionText.text.Length; i++)
        {
            questionText.maxVisibleCharacters++;
            yield return new WaitForSeconds(timePerCharacter);
        }

        ShowOptions();
    }

    public void ShowOptions()
    {
        for(int i = 0; i < tsismisQuestionaire[questionNumber].Answers.Length; i++)
        {
            Options[i].SetActive(true);
            Options[i].GetComponentInChildren<TextMeshProUGUI>().text = tsismisQuestionaire[questionNumber].Answers[i].answer;
            Options[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = tsismisQuestionaire[questionNumber].Answers[i].explanation;
            Options[i].GetComponentsInChildren<Image>()[1].sprite = tsismisQuestionaire[questionNumber].Answers[i].chibi;
        }
    }

    public void AnswerCheck(int answerbox)
    {
        foreach (GameObject options in Options)
            options.SetActive(false);

        Debug.Log(Options[answerbox].GetComponentInChildren<TextMeshProUGUI>().text);

        if(Options[answerbox].GetComponentInChildren<TextMeshProUGUI>().text == tsismisQuestionaire[questionNumber].correctAnswer)
        {
            Debug.Log("correct answer");
            AudioManager.instance.PlaySFX(sfxenum.Male1_MakesSense);
            correct++;
            CheckFragment();
            Debug.Log(questionNumber);
            StartCoroutine(typeRightText(tsismisQuestionaire[questionNumber].RightDialogue));
        }
        else
        {
            Debug.Log("Wrong answer");
            AudioManager.instance.PlayRandomSFX(new List<sfxenum> { sfxenum.Male1_Huh1, sfxenum.Male1_Huh2 });
            StartCoroutine(typeWrongText(WrongAnswer));
        }
    }

    private IEnumerator typeWrongText(string textToDisplay)
    {
        textIsDone = false;
        questionText.maxVisibleCharacters = 0;
        questionText.text = textToDisplay;

        for (int i = 0; i < questionText.text.Length; i++)
        {
            questionText.maxVisibleCharacters++;
            yield return new WaitForSeconds(timePerCharacter);
        }

        yield return new WaitForSeconds(1);
        yield return StartCoroutine(typeQuestion(tsismisQuestionaire[questionNumber].Question));
        textIsDone = false;
    }

    private IEnumerator typeRightText(string textToDisplay)
    {
        questionText.maxVisibleCharacters = 0;
        questionText.text = textToDisplay;

        for (int i = 0; i < questionText.text.Length; i++)
        {
            questionText.maxVisibleCharacters++;
            yield return new WaitForSeconds(timePerCharacter);
        }

        yield return new WaitForSeconds(1);

        if (questionNumber == 0)
        {
            // next day
            StartCoroutine(typeNextDay(NextDay));
        }
        else if (questionNumber == 1)
        {
            questionNumber = 2;
            ShowQuestion();
        }
        else
        {
            GameManager.instance.StartNewDay();
        }
    }

    IEnumerator typeNextDay(string textToDisplay)
    {
        questionText.maxVisibleCharacters = 0;
        questionText.text = textToDisplay;

        for (int i = 0; i < questionText.text.Length; i++)
        {
            questionText.maxVisibleCharacters++;
            yield return new WaitForSeconds(timePerCharacter);
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(GameManager.instance.GameToTsismis());

        // start next day
    }

    void CheckFragment()
    {
        for(int i = 0; i < correct; i++)
        {
            fragments[i].SetActive(true);
        }
    }
}

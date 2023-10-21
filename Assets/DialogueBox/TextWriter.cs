using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextWriter : MonoBehaviour
{
    [SerializeField] private float timePerCharacter;
    [SerializeField] private int textID = 0;
    [SerializeField] private Sprite[] textBoxSprites;
    private TextMeshProUGUI characterTextBox;
    private Dialogue[] currentDialogue;
    private Image textBoxBG;

    // Start is called before the first frame update
    void Start()
    {
        textBoxBG = GameObject.Find("BG").GetComponent<Image>();

        characterTextBox = GetComponentInChildren<TextMeshProUGUI>();
        characterTextBox.maxVisibleCharacters = 0;

        StartCoroutine(typeText(currentDialogue[textID].message, timePerCharacter));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDialogue (Dialogue[] dialogueToDisplay)
    {
        currentDialogue = dialogueToDisplay;
    }

    public void ProgressDialogue()
    {
        if (textID < currentDialogue.Length - 1)
        {
            characterTextBox.maxVisibleCharacters = 0;
            textID++;
            textBoxBG.sprite = textBoxSprites[currentDialogue[textID].actorId];
            StartCoroutine(typeText(currentDialogue[textID].message, timePerCharacter));
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator typeText(string textToDisplay, float timePerCharacter)
    {
        characterTextBox.text = textToDisplay;

        for (int i = 0; i < characterTextBox.text.Length; i++)
        {
            characterTextBox.maxVisibleCharacters++;
            yield return new WaitForSeconds(timePerCharacter);
        }
    }
}

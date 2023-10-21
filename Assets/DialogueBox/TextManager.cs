using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{
    [SerializeField] private string[] characterDialogue;
    [SerializeField] private float timePerCharacter;
    [SerializeField] private int textID = 0;
    private TextMeshProUGUI characterTextBox;

    // Start is called before the first frame update
    void Start()
    {
        characterTextBox = GetComponentInChildren<TextMeshProUGUI>();
        characterTextBox.maxVisibleCharacters = 0;
        StartCoroutine(typeText(characterDialogue[textID], timePerCharacter));
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ProgressDialogue()
    {
        if (textID < characterDialogue.Length - 1)
        {
            characterTextBox.maxVisibleCharacters = 0;
            textID++;
            StartCoroutine(typeText(characterDialogue[textID], timePerCharacter));
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

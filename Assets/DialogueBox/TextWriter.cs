using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum DialgoueTypes
{
    LEAVE, 
    ENTER,
    WRONG,
    LEAVE_BAD
}

public class TextWriter : MonoBehaviour
{
    [SerializeField] private float timePerCharacter;
    [SerializeField] private int textID = 0;
    [SerializeField] private Sprite[] textBoxSprites;
    private TextMeshProUGUI characterTextBox;
    private Dialogue[] currentDialogue;
    [SerializeField] private Image textBoxBG;


    // Start is called before the first frame update
    void Start()
    {
        textBoxBG = GameObject.Find("BG").GetComponent<Image>();

        characterTextBox = GetComponentInChildren<TextMeshProUGUI>();
        characterTextBox.maxVisibleCharacters = 0;

        StartCoroutine(typeText(currentDialogue[textID].message, timePerCharacter));

        GameManager.instance.Player.GetComponent<Player>().isTalking = true;
        AudioManager.instance.PlaySFX(currentDialogue[textID].voiceLine);
        GameManager.instance.Customer.GetComponent<SpriteRenderer>().sprite = currentDialogue[textID].characterSprite;

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
            AudioManager.instance.PlaySFX(currentDialogue[textID].voiceLine);
            GameManager.instance.Customer.GetComponent<SpriteRenderer>().sprite = currentDialogue[textID].characterSprite;

            textBoxBG.sprite = textBoxSprites[currentDialogue[textID].actorId];
            StartCoroutine(typeText(currentDialogue[textID].message, timePerCharacter));
        }

        else
        {
            GameManager.instance.Player.GetComponent<Player>().isTalking = false;
            switch (GameManager.instance.Customer.GetComponent<DialogActivator>().dialgoueTypes)
            {
                case DialgoueTypes.ENTER:

                    break;

                case DialgoueTypes.LEAVE_BAD:

                    break;

                case DialgoueTypes.WRONG:

                    break;

                case DialgoueTypes.LEAVE:
                    GameManager.instance.CallNextCustomer();
                    AudioManager.instance.PlaySFX(sfxenum.sfx_customerSuccessful1);
                    break;
            }

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

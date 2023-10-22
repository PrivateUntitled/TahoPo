using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Dialogue 
{
    public int actorId; // 0 - customer, 1 - taho man speech, 2 - taho man thot
    public string message;
    public sfxenum voiceLine;
    public Sprite characterSprite;
}

public class DialogActivator : MonoBehaviour
{
    [SerializeField] private GameObject dialogManagerPrefab;
    private GameObject dialogBox;
    [SerializeField] private TextWriter textWriter;
    public TextWriter TextWriter { get{ return textWriter; } }

    [Header("Customer Dialogue Per Day")]
    [SerializeField] private Dialogue[] openingDialogueDay1;
    [SerializeField] private Dialogue[] openingDialogueDay2;
    [SerializeField] private Dialogue[] correctDialogueDay1;
    [SerializeField] private Dialogue[] correctDialogueDay2;

    [Header("Common Dialogue")]
    [SerializeField] private Dialogue[] wrongOrderDialogue;
    [SerializeField] private Dialogue[] walkOutDialogue;

    public DialgoueTypes dialgoueTypes;

    // Start is called before the first frame update
    void Start()
    {
        CreateTextBox();
        BeforeServeDialogue(GameManager.instance.CurrentDay);
    }

    public void Init(DialgoueTypes _dialogueTypes)
    {
        dialgoueTypes = _dialogueTypes;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateTextBox()
    {
        dialogBox = Instantiate(dialogManagerPrefab, dialogManagerPrefab.transform.position, Quaternion.identity);
        textWriter = dialogBox.GetComponent<TextWriter>();
        SceneManager.MoveGameObjectToScene(dialogBox, SceneManager.GetSceneByName("MainGame"));
    }

    void BeforeServeDialogue(int dayCount)
    {
        dialgoueTypes = DialgoueTypes.ENTER;
        if (dayCount == 0) 
        {
            GameManager.instance.Customer.GetComponent<SpriteRenderer>().sprite = openingDialogueDay1[0].characterSprite;
            textWriter.SetDialogue(openingDialogueDay1);
        }

        else if (dayCount == 1) 
        {
            GameManager.instance.Customer.GetComponent<SpriteRenderer>().sprite = openingDialogueDay2[0].characterSprite;
            textWriter.SetDialogue(openingDialogueDay2);
        }
    }

    public void AfterServeDialogue(int dayCount)
    {
        dialgoueTypes = DialgoueTypes.LEAVE;
        CreateTextBox();
        if (dayCount == 0) textWriter.SetDialogue(correctDialogueDay1);
        else if (dayCount == 1) textWriter.SetDialogue(correctDialogueDay2);
    }

    public void LeaveDialogue(int tryCount)
    {
        CreateTextBox();

        if (tryCount > 0)
        {
            dialgoueTypes = DialgoueTypes.WRONG;
            textWriter.SetDialogue(wrongOrderDialogue);
        }

        else
        {
            dialgoueTypes = DialgoueTypes.LEAVE_BAD;
            textWriter.SetDialogue(walkOutDialogue);
        }
    }
}
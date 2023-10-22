using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Dialogue 
{
    public int actorId; // 0 - customer, 1 - taho man speech, 2 - taho man thot
    public string message;
}

public class DialogActivator : MonoBehaviour
{
    [SerializeField] private GameObject dialogManagerPrefab;
    private GameObject dialogBox;
    [SerializeField] private int currentDay = 1;
    [SerializeField] private TextWriter textWriter;

    [Header("Customer Dialogue Per Day")]
    [SerializeField] private Dialogue[] openingDialogueDay1;
    [SerializeField] private Dialogue[] openingDialogueDay2;
    [SerializeField] private Dialogue[] correctDialogueDay1;
    [SerializeField] private Dialogue[] correctDialogueDay2;

    [Header("Common Dialogue")]
    [SerializeField] private Dialogue[] wrongOrderDialogue;
    [SerializeField] private Dialogue[] walkOutDialogue;


    // Start is called before the first frame update
    void Start()
    {
        CreateTextBox();
        BeforeServeDialogue(currentDay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateTextBox()
    {
        dialogBox = Instantiate(dialogManagerPrefab, dialogManagerPrefab.transform.position, Quaternion.identity);
        textWriter = dialogBox.GetComponent<TextWriter>();
        //SceneManager.MoveGameObjectToScene(dialogBox, SceneManager.GetSceneByName("Main Game"));
    }

    void BeforeServeDialogue(int dayCount)
    {
        if (dayCount == 1) textWriter.SetDialogue(openingDialogueDay1);
        else if (dayCount == 2) textWriter.SetDialogue(openingDialogueDay2);
    }

    public void AfterServeDialogue(int dayCount)
    {
        CreateTextBox();
        if (dayCount == 1) textWriter.SetDialogue(correctDialogueDay1);
        else if (dayCount == 2) textWriter.SetDialogue(correctDialogueDay2);
    }

    public void LeaveDialogue(int tryCount)
    {
        CreateTextBox();
        if (tryCount > 0) textWriter.SetDialogue(wrongOrderDialogue);
        else textWriter.SetDialogue(walkOutDialogue);
    }
}
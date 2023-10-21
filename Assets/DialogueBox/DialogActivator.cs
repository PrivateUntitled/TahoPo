using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int currentDay = 2;
    [SerializeField] private TextWriter textWriter;

    [Header("Customer Dialogue")]
    [SerializeField] private Dialogue[] openingDialogueDay1;
    [SerializeField] private Dialogue[] openingDialogueDay2;
    [SerializeField] private Dialogue[] endingDialogueDay1;
    [SerializeField] private Dialogue[] endingDialogueDay2;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox = Instantiate(dialogManagerPrefab, dialogManagerPrefab.transform.position, Quaternion.identity);
        textWriter = dialogBox.GetComponent<TextWriter>();
        OpeningDialogue(currentDay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpeningDialogue(int dayCount)
    {
        if (currentDay == 1) textWriter.SetDialogue(openingDialogueDay1);
        else if (currentDay == 2) textWriter.SetDialogue(openingDialogueDay2);
    }

    public void EndingDialogue(int dayCount)
    {
        dialogBox = Instantiate(dialogManagerPrefab, dialogManagerPrefab.transform.position, Quaternion.identity);
        textWriter = dialogBox.GetComponent<TextWriter>();
        if (currentDay == 1) textWriter.SetDialogue(endingDialogueDay1);
        else if (currentDay == 2) textWriter.SetDialogue(endingDialogueDay2);
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public GameObject dialoguePanel;

    [SerializeField]
    private Dialogue[] dialogues;
    private int currentDialogueIndex = 0;
    private bool isTyping = false;


    private void Start()
    {
        SetDialogues(dialogues);
    }

    //外部调用
    public void SetDia()
    {
        SetDialogues(dialogues);
    }

    // 设置对话数组
    public void SetDialogues(Dialogue[] newDialogues)
    {
        dialogues = newDialogues;
        currentDialogueIndex = 0;
        StartDialogue();
    }

    private void StartDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            StartCoroutine(TypeSentence(dialogues[currentDialogueIndex].dialogueText));
            nameText.text = dialogues[currentDialogueIndex].speakerName;
        }
        else
        {
            EndDialogue();
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // 调整速度
        }
        isTyping = false;
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTyping && Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == dialogues[currentDialogueIndex].dialogueText)
            {
                currentDialogueIndex++;
                StartDialogue();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogues[currentDialogueIndex].dialogueText;
                isTyping = false;
            }
        }
    }
}

[System.Serializable]
public class Dialogue
{
    public string speakerName;
    public string dialogueText;
}

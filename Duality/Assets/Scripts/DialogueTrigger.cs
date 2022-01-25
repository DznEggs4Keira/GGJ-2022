using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Dialogue m_dialogue;

    public void TriggerDialogue() {
        
        var dialogueManager = FindObjectOfType<DialogueManager>();

        dialogueManager.nameText = nameText;
        dialogueManager.dialogueText = dialogueText;
        dialogueManager.StartDialogue(m_dialogue);
    }
}

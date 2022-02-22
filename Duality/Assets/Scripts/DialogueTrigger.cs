using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Dialogue m_dialogue;

    public Animator animator;

    public void TriggerDialogue(bool item = false) {

        GameManager.instance.dialogueManager.animator = animator;
        GameManager.instance.dialogueManager.nameText = nameText;
        GameManager.instance.dialogueManager.dialogueText = dialogueText;
        GameManager.instance.dialogueManager.StartDialogue(m_dialogue, item);
    }
}

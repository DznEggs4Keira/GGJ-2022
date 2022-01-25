using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue m_dialogue;

    public void TriggerDialogue() {
        FindObjectOfType<DialogueManager>().StartDialogue(m_dialogue);
    }
}

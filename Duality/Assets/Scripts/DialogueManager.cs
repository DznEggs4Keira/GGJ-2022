using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private Queue<string> sentences;

    private void Start() {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {

        nameText.text = dialogue.m_Name;

        sentences.Clear();

        foreach (string sentence in dialogue.m_sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {

        if(sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

    }

    private void EndDialogue() {
        Debug.Log("End of conversation");
    }
}

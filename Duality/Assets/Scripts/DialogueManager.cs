using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    private void Start() {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {

        Debug.Log($"Started conversation with {dialogue.m_Name}");

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
        Debug.Log(sentence);

    }

    private void EndDialogue() {
        Debug.Log("End of conversation");
    }
}

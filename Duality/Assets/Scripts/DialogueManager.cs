using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    public Collider2D npcCollider;

    private bool itemRecieved = false;
    private Queue<string> sentences;
    private bool dialogueEnded = false;

    public bool DialogueEnded {
        get { return dialogueEnded; }
        set { dialogueEnded = value; }
    }

    private void Start() {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, bool item = false) {

        animator.SetBool("isOpen", true);

        nameText.text = dialogue.m_Name;

        itemRecieved = item;

        sentences.Clear();

        foreach (string sentence in dialogue.m_sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {

        DialogueEnded = false;

        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence) {

        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue() {
        npcCollider.enabled = true;
        DialogueEnded = true;
        animator.SetBool("isOpen", false);

        //if intro dialogue, then increment without item recieved
        if (GameManager.checkpoint == 0) {
            GameManager.checkpoint++;
            return;
        }

        if (itemRecieved) {
            GameManager.checkpoint++;
            // set item recieved back to false until we get the next item
            itemRecieved = false;
        }

        
    }
}

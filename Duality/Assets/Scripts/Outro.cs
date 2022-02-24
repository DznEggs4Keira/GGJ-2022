using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outro : MonoBehaviour
{
    public Transform GrandmaModel;
    public Transform UVModel;
    public Transform Colliders;

    public SpriteRenderer WillowSR;
    public Sprite GrannyWillow;
    public Transform WillowModel;

    public AudioClip newClip;

    public ParticleSystem particles;

    private bool isOutroInit = false;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.checkpoint == 7 && !isOutroInit) {
            InitiateOutro();
        }

        if (isOutroInit && GameManager.instance.dialogueManager.DialogueEnded) {
            StartCoroutine(FadeOutExit());
        }
    }

    private void InitiateOutro() {

        isOutroInit = true;

        // fade music
        GameManager.instance.audioManager.SwapTrack(newClip);

        // start particles
        particles.Play();

        // turn granny to uv
        Colliders.gameObject.SetActive(false);
        GrandmaModel.gameObject.SetActive(false);
        GrandmaModel.gameObject.SetActive(true);

        // turn off animator and turn willow to granny
        WillowSR.transform.gameObject.GetComponentInChildren<Animator>().enabled = false;
        WillowSR.sprite = GrannyWillow;

        Vector3 newScaleForWillow = new Vector3(0.25f, 0.25f, 1f);
        WillowModel.localScale = newScaleForWillow;

        WillowModel.GetComponentInParent<Player>().disablePlayer = true;

        UVModel.GetComponentInParent<Grandma>().TriggerDialogueManual();
    }

    IEnumerator FadeOutExit() {
        yield return new WaitForSeconds(3);

        GameManager.instance.fadeManager.Fade();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outro : MonoBehaviour
{

    public SpriteRenderer GrandmaSR;
    public Transform UVModel;
    public Sprite UltraViolet;

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
            UVModel.GetComponentInParent<Grandma>().TriggerDialogueManual();
        }
    }

    private void InitiateOutro() {

        isOutroInit = true;

        // fade music
        GameManager.instance.audioManager.SwapTrack(newClip);

        // start particles
        particles.Play();

        // turn granny to uv
        GrandmaSR.sprite = UltraViolet;

        Vector3 newScale = new Vector3(0.4f, 0.4f, 1f);
        UVModel.localScale = newScale;

        // turn off animator and turn willow to granny
        WillowSR.transform.gameObject.GetComponentInChildren<Animator>().enabled = false;
        WillowSR.sprite = GrannyWillow;

        Vector3 newScaleForWillow = new Vector3(0.25f, 0.25f, 1f);
        WillowModel.localScale = newScaleForWillow;

        WillowModel.GetComponentInParent<Player>().disablePlayer = true;

        StartCoroutine(FadeOutExit(30));
    }

    IEnumerator FadeOutExit(float delay) {
        yield return new WaitForSeconds(delay);

        GameManager.instance.fadeManager.Fade();
    }
}

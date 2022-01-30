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

    // Update is called once per frame
    void Update()
    {
        if(Grandma.checkpoint == 8) {
            InitiateOutro();
        }
    }

    private void InitiateOutro() {

        // fade music
        AudioManager.instance.SwapTrack(newClip);

        // start particles

        // turn granny to uv
        GrandmaSR.sprite = UltraViolet;

        Vector3 newScale = new Vector3(0.4f, 0.4f, 1f);
        UVModel.localScale = newScale;

        // turn off animator and turn willow to granny
        WillowSR.transform.gameObject.GetComponentInChildren<Animator>().enabled = false;
        WillowSR.sprite = GrannyWillow;

        Vector3 newScaleForWillow = new Vector3(0.4f, 0.4f, 1f);
        WillowModel.localScale = newScaleForWillow;

        WillowSR.transform.gameObject.GetComponentInParent<CharacterMovement>().enabled = false;

    }
}

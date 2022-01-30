using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outro : MonoBehaviour
{
    public SpriteRenderer GrandmaSR;
    public Sprite UltraViolet;

    public SpriteRenderer WillowSR;
    public Sprite GrannyWillow;

    // Update is called once per frame
    void Update()
    {
        if(Grandma.checkpoint == 8) {
            InitiateOutro();
        }
    }

    private void InitiateOutro() {

        // fade music

        // start particles

        // turn granny to uv
        GrandmaSR.sprite = UltraViolet;

        // turn willow to granny
        WillowSR.sprite = GrannyWillow;

    }
}

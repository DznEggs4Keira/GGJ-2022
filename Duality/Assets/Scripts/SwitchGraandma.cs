using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGraandma : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite grandmaKnitting;
    public Sprite grandmaPondering;

    private void OnTriggerEnter2D(Collider2D collision) {
        spriteRenderer.sprite = grandmaKnitting;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        spriteRenderer.sprite = grandmaPondering;
    }
}

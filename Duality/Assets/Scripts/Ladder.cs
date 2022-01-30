using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : Interactable
{
    [SerializeField] Transform Top;
    [SerializeField] Transform Bottom;
    [SerializeField] Player Player;

    public override string GetDescription() {
        return "Hold E to use Stairs";
    }

    public override void Interact() {
     var hit = Physics2D.OverlapCircle(Player.controller.PlayerFeet.position, 0.1f, LayerMask.GetMask("Interactables"));
        if(hit != null) {

            //if hit is Bottom, send to top, vice versa
            if(hit.gameObject.name == Bottom.gameObject.name) {
                Player.controller.TeleportTo(Top.transform.position);
            } else if (hit.gameObject.name == Top.gameObject.name) {
                Player.controller.TeleportTo(Bottom.transform.position);
            }
        }
    }
}

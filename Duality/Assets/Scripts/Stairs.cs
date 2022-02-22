using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs: MonoBehaviour
{
    [SerializeField] Player Player;

    [SerializeField] PlatformEffector2D connectedFloorEffector;

    private void Update() {

        //if true
        if(Player.controller.ClimbingAllowed) {
            if (Input.GetAxis($"Vertical") > 0) {
                //going up then the offset has to be zero
                connectedFloorEffector.rotationalOffset = 0;
            } else if (Input.GetAxis($"Vertical") < 0) {
                //going down then the offset has to be 180
                connectedFloorEffector.rotationalOffset = 180;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //climbing enabled
        Player.controller.ClimbingAllowed = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        //climbing disabled
        Player.controller.ClimbingAllowed = false;
        connectedFloorEffector.rotationalOffset = 0;
    }
}

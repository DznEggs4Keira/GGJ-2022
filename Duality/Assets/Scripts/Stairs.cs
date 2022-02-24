using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs: MonoBehaviour
{
    [SerializeField] Player Player;

    [SerializeField] PlatformEffector2D connectedFloorEffector;

    private float waitTime = 0.5f;

    private void Update() {

        //if true
        if(Player.controller.ClimbingAllowed) {
            if (Input.GetAxis($"Vertical") > 0) {
                //going up then the offset has to be zero
                connectedFloorEffector.rotationalOffset = 0;
            } else if (Input.GetAxis($"Vertical") < 0) {

                StartCoroutine(ResetFloor(waitTime));
            }

            //else if (Mathf.Approximately(Input.GetAxis($"Vertical"), 0)) {
            //    connectedFloorEffector.rotationalOffset = 0;
            //}
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
        StopAllCoroutines();
    }

    IEnumerator ResetFloor(float delay) {
        //going down then the offset has to be 180
        connectedFloorEffector.rotationalOffset = 180;

        yield return new WaitForSeconds(waitTime);

        connectedFloorEffector.rotationalOffset = 0;
    }
}

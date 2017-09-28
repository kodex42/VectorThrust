using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
    private Player player;

    private void Start() {
        player = gameObject.GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        player.grounded = true;
        player.canDoubleJump = false;

        if (collision.tag == "KillBounds") {
            player.dealDamage(100);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        player.grounded = true;
        player.canDoubleJump = false;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        player.grounded = false;
        player.canDoubleJump = true;
    }
}

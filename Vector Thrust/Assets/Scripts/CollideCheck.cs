using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideCheck : MonoBehaviour {
    private Player player;

    void Start () {
        player = gameObject.GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        player.forceOutStasis = false;
        player.timeManager.slowMo = false;

        if (collision.tag == "KillBounds") {
            player.dealDamage(100);
        }
    }
}

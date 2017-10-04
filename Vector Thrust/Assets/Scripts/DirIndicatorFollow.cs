using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirIndicatorFollow : MonoBehaviour {
    private Vector2 velocity;
    public Player player;
    public float angleX;
    public float angleY;
    public float angleRad;

    // Use this for initialization
    void Start() {
        player = gameObject.GetComponentInParent<Player>();
    }

    void Update() {
        angleX = Input.GetAxis("Horizontal");
        angleY = Input.GetAxis("Vertical");

        angleRad = Mathf.Atan2(-angleX, -angleY) * 180 / Mathf.PI;
        float posX = player.transform.position.x;
        float posY = player.transform.position.y;

        transform.position = new Vector3(posX, posY);
        //transform.Rotate(new Vector3(posX, posY, angle));
        transform.eulerAngles = new Vector3(0, 0, angleRad);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour {

    public bool canMove;
    public float speed;
    private float moveHorizontal;
    private Rigidbody rb;
    public GameObject ball;

    void Start() {
        canMove = true;
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (canMove) {
            moveHorizontal = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(moveHorizontal, 0, 0);
            rb.velocity = (movement * speed);
        } else {
            transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y, transform.position.z);
        }
    }

}

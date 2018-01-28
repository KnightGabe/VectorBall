using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour {

    public bool canMove;
    public float speed;
    private float moveHorizontal;
    private float moveVertical;
    private Rigidbody rb;
    public GameObject ball;
    public Transform farLeftCamera;
    public Transform farRightCamera;
    public Transform farUpCamera;
    public Transform farDownCamera;

    void Start() {
        canMove = true;
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (canMove) {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);
            rb.velocity = (movement * speed);
            if (transform.position.x > farRightCamera.position.x - 890f) {
                transform.position = new Vector3(farRightCamera.position.x - 890f, transform.position.y, transform.position.z);
            } else {
                if (transform.position.x < farLeftCamera.position.x + 890f) {
                    transform.position = new Vector3(farLeftCamera.position.x + 890f, transform.position.y, transform.position.z);
                }
            }
            if (transform.position.y > farUpCamera.position.y - 500f) {
                transform.position = new Vector3(transform.position.x, farUpCamera.position.y - 500f, transform.position.z);
            } else {
                if (transform.position.y < farDownCamera.position.y + 500f) {
                    transform.position = new Vector3(transform.position.x, farDownCamera.position.y + 500f, transform.position.z);
                }
            }
        } else {
            transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y, transform.position.z);
        }
    }

}

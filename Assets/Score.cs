using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    private int point;

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Coletavel") {
            Debug.Log("Pontos");
            point = other.GetComponent<Coletavel>().pointValue;
            GameObject.FindObjectOfType<GameManager>().SetScore(point);
            other.GetComponent<Coletavel>().SelfDestruct();
        }
    }
}

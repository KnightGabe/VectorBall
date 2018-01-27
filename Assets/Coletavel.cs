using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletavel : MonoBehaviour {

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Coletavel") {
            Debug.Log("pontos");
        }
    }
}

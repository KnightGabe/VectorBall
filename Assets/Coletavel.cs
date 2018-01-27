using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletavel : MonoBehaviour {

    public GameObject Particula;

    public int pointValue;

    public void SelfDestruct() {
        GameObject clone = Instantiate(Particula, transform.position, Quaternion.identity);
        Destroy(clone, 2f);
        Destroy(gameObject);
    }
}

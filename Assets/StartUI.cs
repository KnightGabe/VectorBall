using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour {

    public Text cd;
    [SerializeField]
    GameManager gm;


    void Update() {
        if (gm.TimeToStart > 0) {
            cd.text = ((int)gm.TimeToStart).ToString();
        } else {
            cd.gameObject.SetActive(false);
        }
    }
}

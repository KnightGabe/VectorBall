using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour {
    
    public Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    } 

    public void ProxFase() {
        SceneManager.LoadScene(1);
    }

    public void StartGame() {
        anim.SetTrigger("Go");
    }
}

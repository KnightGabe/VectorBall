using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public RectTransform ScoreBar;

    public GameObject TimeUI;

    private int score;
    private int maxScore;
    private bool Wait;

    public float TimeToStart = 4f;


    void Start () {
        ScoreBar.localScale = new Vector3(0, ScoreBar.localScale.y, ScoreBar.localScale.z);
        maxScore = 5;
        score = 0;
        Wait = false;
    }

    void Update() {
        if (Wait) {
            if (TimeToStart <= 0) {
                GameObject _ball = GameObject.FindGameObjectWithTag("Bola");
                _ball.GetComponent<Rigidbody2D>().gravityScale = 1;
            } else {
                TimeToStart -= Time.deltaTime;
            }
        }
    }

    public void cantMoveCam() {
        Camera.main.GetComponent<moveCamera>().canMove = false;
        Wait = true;
        TimeUI.SetActive(true);
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void SetScore(int _score) {
        Debug.Log("To aqui e " + score + " / " + maxScore);
        if (score < maxScore) {
            Debug.Log(score + " / " + maxScore);
            score += _score;
            Debug.Log(score);
            float curScore = (float)score / maxScore;
            Debug.Log(curScore);
            ScoreBar.localScale = new Vector3(curScore, ScoreBar.localScale.y, ScoreBar.localScale.z);
        }
    }
}

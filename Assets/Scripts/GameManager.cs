using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public RectTransform ScoreBar;
	public Slider EnergyBar;

	VectorScript currentVector;

	AudioSource source;

	public AudioClip[] clicks;

	Vector3 mousePos;

    public GameObject TimeUI;
	public GameObject vector;

	public float energyValue;

	private int clickCount;
    private int score;
    private int maxScore;
    private bool setVector, clicked;
	public bool deleteVector, Wait, energyDecrease;

    public float TimeToStart = 4f;


    void Start () {
        ScoreBar.localScale = new Vector3(0, ScoreBar.localScale.y, ScoreBar.localScale.z);
        maxScore = 5;
        score = 0;
        Wait = false;
		EnergyBar.maxValue = energyValue;
		source = GetComponent<AudioSource>();
    }

	public void PlayClickSound()
	{
		source.PlayOneShot(clicks[Random.Range(0, clicks.GetLength(0))]);
	}

	public void EnableDelete()
	{
		deleteVector = true;
	}

	public void RestoreEnergy(float energyCount)
	{
		energyValue += energyCount;
	}

    void Update() {
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			clicked = true;
		}
		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			clicked = false;
			clickCount++;
		}
		DrainEnergy();
		EnergyBar.value = Mathf.Clamp(energyValue, 0, EnergyBar.maxValue);
        if (Wait) {
            if (TimeToStart <= 0) {
                GameObject _ball = GameObject.FindGameObjectWithTag("Bola");
                _ball.GetComponent<Rigidbody2D>().gravityScale = 1;
            } else {
                TimeToStart -= Time.deltaTime;
            }
		}
		else if(setVector) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Plane hPlane = new Plane(Vector3.back, transform.position);
			float dist = 0;
			if (hPlane.Raycast(ray, out dist))
			{
				mousePos = ray.GetPoint(dist);
			}
			if (clicked && currentVector == null || energyValue > 0 && clickCount >= 2 && currentVector.beenSet )
			{
				GameObject newVector = Instantiate(vector, mousePos, Quaternion.identity);
				currentVector = newVector.GetComponent<VectorScript>();
				energyDecrease = false;				
				setVector = false;
				clickCount = 0;
			}
		}

		Debug.Log(energyValue);
		Debug.Log(currentVector.vetorResultante.magnitude);
	}

	public void DrainEnergy()
	{
		if (currentVector != null)
		{
			if (!energyDecrease && currentVector.beenSet)
			{
				energyValue -= currentVector.vetorResultante.magnitude;
				energyDecrease = true;
			}
		}
	}
	public void SetCommand(int commandID)
	{
		clickCount = 0;
		if (!Wait)
		{
			setVector = true;
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

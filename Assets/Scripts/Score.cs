using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    private int point, i;
	public AudioClip[] coletavelGet;
	AudioSource source;

	private void Start()
	{
		source = GetComponent<AudioSource>();
	}

	public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Coletavel")
		{
			Debug.Log("Pontos");
            point = other.GetComponent<Coletavel>().pointValue;
            GameObject.FindObjectOfType<GameManager>().SetScore(point);
			source.PlayOneShot(coletavelGet[i]);
			i++;
			other.GetComponent<Coletavel>().SelfDestruct();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScriptBola : MonoBehaviour {

	Rigidbody2D rigid;

	bool reversed;

	public AudioSource speedAudio;

	public float velocidade, velocidadeMax, volumeMod, pitchMod;
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		//speedAudio.volume = Mathf.Clamp(1-(Mathf.Abs(1/ rigid.velocity.magnitude)), 0.3f, 1);
		//speedAudio.DOKill();
		speedAudio.DOFade(Mathf.Clamp(1 - (Mathf.Abs(1 / rigid.velocity.magnitude)), 0.3f, 1), 0.5f);
		speedAudio.DOPitch(Mathf.Clamp(1.5f - (Mathf.Abs(1 / rigid.velocity.magnitude)), 1, 2), 0.5f);
		//speedAudio.pitch = Mathf.Clamp(1.5f -(Mathf.Abs(1 / rigid.velocity.magnitude)), 1, 2);
		if (rigid.velocity.x < velocidadeMax || rigid.velocity.x < -velocidadeMax)
		{
			if (!reversed)
			{
				rigid.AddTorque(-velocidade, ForceMode2D.Force);
			}
			else
			{
				rigid.AddTorque(velocidade, ForceMode2D.Force);
			}
		}
	}
}

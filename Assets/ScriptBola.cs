using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBola : MonoBehaviour {

	Rigidbody2D rigid;

	bool reversed;

	public float velocidade, velocidadeMax;
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
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

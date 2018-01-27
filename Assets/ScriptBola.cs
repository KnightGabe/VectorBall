using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBola : MonoBehaviour {

	public bool emContato = false;
	public bool passou = false;

	Rigidbody rb;
	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	private void OnTriggerExit(Collider other)
	{
		if(emContato && other.gameObject.CompareTag("Vetor"))
		{
			passou = true;
		}
	}
}

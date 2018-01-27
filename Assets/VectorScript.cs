using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorScript : MonoBehaviour {

	GameObject ball;

	Vector3 cliqueInicial, cliqueFinal, vetorResultado;

	public float multiplicador, valorMin, valorMax;

	bool foiUsado = false;

	private void OnMouseDown()
	{
		cliqueInicial = Camera.main.ScreenToViewportPoint(Input.mousePosition);
	}
	private void OnMouseUp()
	{
		cliqueFinal = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		vetorResultado = (cliqueFinal - cliqueInicial) * multiplicador;
		Debug.Log((vetorResultado) * multiplicador);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Bola")&& !foiUsado)
		{
			ball = other.gameObject;
			Rigidbody ballRigid = ball.GetComponent<Rigidbody>();
			ballRigid.velocity = new Vector3(0, 0, 0);
			Invoke("VectorJump", 0.5f);
		}
	}
	void VectorJump(Collider other)
	{
		float x = Mathf.Clamp(vetorResultado.x, valorMin, valorMax);
		float y = Mathf.Clamp(vetorResultado.y, valorMin, valorMax);
		Rigidbody ballRigid = ball.GetComponent<Rigidbody>();
		//ballRigid.velocity = new Vector3(0, 0, 0);
		ballRigid.AddForce(new Vector3(x, y, 0), ForceMode.Impulse);
		foiUsado = true;
	}
}

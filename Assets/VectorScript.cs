using UnityEngine;

public class VectorScript : MonoBehaviour
{
	bool clicked;
	public bool foiUsado;
	Vector3 posicaoInicial;
	public LayerMask bolaLayer;
	public float maxVectorDistance;
	SpringJoint spring;
	GameObject ball;
	// Use this for initialization
	void Start()
	{
		posicaoInicial = transform.position;
		spring = GetComponent<SpringJoint>();
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, transform.position.x - maxVectorDistance, transform.position.x + maxVectorDistance),
			Mathf.Clamp(transform.position.y, transform.position.y - maxVectorDistance, transform.position.y + maxVectorDistance), 0);
		ProcurarBola();
		if (clicked)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Plane hPlane = new Plane(Vector3.back, transform.position);
			float dist = 0;
			if (hPlane.Raycast(ray, out dist))
			{
				transform.position = ray.GetPoint(dist);
			}
		}
	}
	void ProcurarBola()
	{
		Collider[] bola = Physics.OverlapSphere(posicaoInicial, 0.1f, bolaLayer);
		foreach (Collider elemento in bola)
		{
			Debug.Log(elemento);
			if (!foiUsado)
			{
				ball = elemento.gameObject;
				spring.connectedBody = ball.GetComponent<Rigidbody>();
				Invoke("DesconectaVetor", 0.5f);
			}
		}
	}

	void DesconectaVetor()
	{
		spring.connectedBody = null;
		foiUsado = true;
	}

	private void OnMouseDown()
	{
		clicked = true;
	}
	private void OnMouseUp()
	{
		clicked = false;
	}
}
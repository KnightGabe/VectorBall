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
		ProcurarBola();
		if (clicked)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Plane hPlane = new Plane(Vector3.back, transform.position);
			float dist = 0;
			if (hPlane.Raycast(ray, out dist))
			{
				transform.position = new Vector3(Mathf.Clamp(ray.GetPoint(dist).x, posicaoInicial.x - maxVectorDistance, posicaoInicial.x + maxVectorDistance),
			Mathf.Clamp(ray.GetPoint(dist).y, posicaoInicial.y - maxVectorDistance, posicaoInicial.y + maxVectorDistance), 0);
			}
		}
	}
	void ProcurarBola()
	{
		Collider[] bola = Physics.OverlapSphere(posicaoInicial, 1f, bolaLayer);
		foreach (Collider elemento in bola)
		{ 
			if (!foiUsado)
			{
				ball = elemento.gameObject;
				spring.connectedBody = ball.GetComponent<Rigidbody>();
				Invoke("DesconectaVetor", 0.4f);
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
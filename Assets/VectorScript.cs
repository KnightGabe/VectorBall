using UnityEngine;

public class VectorScript : MonoBehaviour
{
	bool clicked;
	public bool foiUsado;
	Vector2 posicaoInicial, posicaoCamera;
	public LayerMask bolaLayer;
	public float maxVectorDistance;
	SpringJoint2D spring;
	GameObject ball;
	// Use this for initialization
	void Start()
	{
		posicaoInicial = transform.position;
		spring = GetComponent<SpringJoint2D>();
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
		Collider2D bola = Physics2D.OverlapCircle(posicaoInicial, 1f, bolaLayer);
		if(bola!= null)
		{ 
			if (!foiUsado)
			{
				ball = bola.gameObject;
				spring.connectedBody = ball.GetComponent<Rigidbody2D>();
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
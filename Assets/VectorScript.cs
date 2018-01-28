using UnityEngine;

public class VectorScript : MonoBehaviour
{
	bool clicked;
	public bool foiUsado, beenSet;
	Vector2 posicaoInicial, posicaoCamera, posicaoFinal;
	public Vector2 vetorResultante;
	public LayerMask bolaLayer;
	public float maxVectorDistance, multiplicador, raioDeteccao;
	//SpringJoint2D spring;
	GameObject ball;
	// Use this for initialization
	void Start()
	{
		posicaoInicial = transform.position;
		//spring = GetComponent<SpringJoint2D>();
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
		Collider2D bola = Physics2D.OverlapCircle(posicaoInicial, raioDeteccao, bolaLayer);
		if(bola!= null)
		{ 
			if (!foiUsado)
			{
				ball = bola.gameObject;
				Rigidbody2D rigid = ball.GetComponent<Rigidbody2D>();
				rigid.velocity = new Vector2(0, 0);
				//spring.connectedBody = ball.GetComponent<Rigidbody2D>();
				Invoke("DesconectaVetor", 0.1f);
				foiUsado = true;
			}
		}
	}

	void DesconectaVetor()
	{
		//spring.connectedBody = null;
		ball.GetComponent<Rigidbody2D>().AddForce((vetorResultante)*multiplicador, ForceMode2D.Impulse);
	}

	private void OnMouseDown()
	{
		if (!beenSet)
		{
			clicked = true;
		}
	}
	private void OnMouseUp()
	{
		clicked = false;
		posicaoFinal = transform.position;
		vetorResultante = new Vector2((((posicaoFinal.x-posicaoInicial.x)/maxVectorDistance))*100, ((posicaoFinal.y-posicaoInicial.y)/maxVectorDistance)*100);
		beenSet = true;
	}
}
using UnityEngine;

public class VectorScript : MonoBehaviour
{
	bool clicked, estaTocando;
	public bool foiUsado, beenSet;
	Vector2 posicaoInicial, posicaoCamera, posicaoFinal;
	public Vector2 vetorResultante;
	public LayerMask bolaLayer;
	public float maxVectorDistance, multiplicador, raioDeteccao;
	AudioSource source;
	AudioSource player;
	GameManager gm;
	public AudioClip[] carregar;
	public AudioClip[] soltar;
	public AudioClip[] colocar;
	public AudioClip[] acelerar;
	
	//SpringJoint2D spring;
	GameObject ball;
	// Use this for initialization
	void Start()
	{
		gm = FindObjectOfType<GameManager>();
		posicaoInicial = transform.position;
		source = GetComponent<AudioSource>();
		source.PlayOneShot(colocar[Random.Range(0, colocar.GetLength(0))]);
		//spring = GetComponent<SpringJoint2D>();
	}

	// Update is called once per frame
	void Update()
	{
		ProcurarBola();
		if(clicked && gm.deleteVector && !gm.Wait)
		{
			gm.deleteVector = false;
			gm.RestoreEnergy(vetorResultante.magnitude);
			Destroy(gameObject);
		}
		if (clicked && !gm.deleteVector)
		{
			TocarClipe();
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
				player = ball.GetComponent<AudioSource>();
				rigid.velocity = new Vector2(0, 0);
				TocarSFXVetor();
				Invoke("DesconectaVetor", 0.1f);
				foiUsado = true;
			}
		}
	}

	void TocarSFXVetor()
	{
		player.PlayOneShot(acelerar[Random.Range(0, acelerar.GetLength(0))]);
	}

	void DesconectaVetor()
	{
		//spring.connectedBody = null;
		ball.GetComponent<Rigidbody2D>().AddForce((vetorResultante)*multiplicador, ForceMode2D.Impulse);
	}

	void TocarClipe()
	{
		if (!estaTocando)
		{
			source.PlayOneShot(carregar[Random.Range(0, carregar.GetLength(0))]);
			estaTocando = true;
		}
	}

	private void OnMouseDown()
	{
		if (!beenSet || gm.deleteVector)
		{
			clicked = true;
		}
	}
	private void OnMouseUp()
	{
		clicked = false;
		float lastEnergy = gm.energyValue;
		posicaoFinal = transform.position;
		vetorResultante = new Vector2((((posicaoFinal.x-posicaoInicial.x)/maxVectorDistance))*100, ((posicaoFinal.y-posicaoInicial.y)/maxVectorDistance)*100);
		if (gm.energyValue - vetorResultante.magnitude < 0)
		{
			gm.energyDecrease = true;
			gm.energyValue = lastEnergy;
			Destroy(gameObject);
			
		}
		source.PlayOneShot(soltar[Random.Range(0, soltar.GetLength(0))]);
		beenSet = true;
	}
}
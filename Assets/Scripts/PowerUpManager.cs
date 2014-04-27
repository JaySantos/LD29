using UnityEngine;
using System.Collections;
using Thinksquirrel.Utilities;

public class PowerUpManager : MonoBehaviour 
{
	private const int CIRCLE_CARD = 0;
	private const int WAVE_CARD = 1;
	private const int CROSS_CARD = 2;
	private const int SQUARE_CARD = 3;
	private const int STAR_CARD = 4;

	public Sprite circleCard;
	public Sprite waveCard;
	public Sprite crossCard;
	public Sprite squareCard;
	public Sprite starCard;

	public int healthRestore = 2;
	public float chanceToDraw = 100;
	public float timeBetweenDrews = 5.0f;

	private float minX = -4.3f;
	private float maxX = 4.3f;
	private float minY = -2.7f;
	private float maxY = 1.3f;

	public GameObject circlePowerUpPrefab;

	private int powerUp;
	private Animator anim;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
	}

	public void Activate()
	{
		gameObject.GetComponent<BoxCollider2D>().enabled = false;
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		InvokeRepeating ("TryToShowCard", 0.0f, timeBetweenDrews);
	}

	void TryToShowCard()
	{
		if (!gameObject.GetComponent<BoxCollider2D>().enabled && !gameObject.GetComponent<SpriteRenderer>().enabled)
		{
			int draw = Random.Range (0, 100);
			if (draw < chanceToDraw)
			{
				powerUp = Random.Range(1, 2);

				switch (powerUp)
				{
				case CIRCLE_CARD:
					anim.SetTrigger("Circle");
					break;
					
				case WAVE_CARD:
					anim.SetTrigger("Wave");
					break;
					
				case CROSS_CARD:
					anim.SetTrigger("Cross");
					break;
					
				case SQUARE_CARD:
					anim.SetTrigger("Square");
					break;
					
				case STAR_CARD:
					anim.SetTrigger("Star");
					break;
				}
			}

			float x = Random.Range(minX, maxX);
			float y = Random.Range(minY, maxY);

			gameObject.transform.position = new Vector2(x, y);

			gameObject.GetComponent<BoxCollider2D>().enabled = true;
			gameObject.GetComponent<SpriteRenderer>().enabled = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			switch (powerUp)
			{
			case CIRCLE_CARD:
				GameObject obj = (GameObject)Instantiate(circlePowerUpPrefab);
				obj.transform.parent = GameObject.Find("Player").transform;

				obj.transform.position = obj.transform.parent.position;

				break;

			case WAVE_CARD:
				GameObject.Find("Player").GetComponent<HeroManager>().EnableStealth();
				break;

			case CROSS_CARD:
				GameObject.Find("Player").GetComponent<HeroManager>().RestoreHealth(healthRestore);
				break;

			case SQUARE_CARD:
				GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");
				foreach (GameObject go in objs)
				{
					Debug.Log("SQUARE:: " + go);
					Debug.Log("SQUARE:: " + go.GetComponent<Enemy>());
					go.GetComponent<Enemy>().IsFrozen = true;
				}
				GameObject.Find("Enemy Manager").GetComponent<EnemySpawner>().IsFrozen = true;
				break;

			case STAR_CARD:
				GameObject cam = GameObject.FindGameObjectWithTag("GameCamera");
				CameraShake cs = cam.GetComponent<CameraShake>();
				cs.shakeAmount = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0f);
				cs.distance = Random.Range(0f, 1f);
				cs.Shake();
				break;
			}
			anim.SetTrigger("Empty");
			gameObject.GetComponent<BoxCollider2D>().enabled = false;
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
	}
}

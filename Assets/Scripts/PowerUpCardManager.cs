using UnityEngine;
using System.Collections;
using Thinksquirrel.Utilities;

public class PowerUpCardManager : MonoBehaviour 
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

	public GameObject circlePowerUpPrefab;

	private int powerUp;

	// Use this for initialization
	void Start () 
	{
		//powerUp = Random.Range(0, 5);
		powerUp = 3;
		switch (powerUp)
		{
		case CIRCLE_CARD:
				gameObject.GetComponent<SpriteRenderer>().sprite = circleCard;
				break;

		case WAVE_CARD:
				gameObject.GetComponent<SpriteRenderer>().sprite = waveCard;
				break;

		case CROSS_CARD:
				gameObject.GetComponent<SpriteRenderer>().sprite = crossCard;
				break;

		case SQUARE_CARD:
				gameObject.GetComponent<SpriteRenderer>().sprite = squareCard;
				break;

		case STAR_CARD:
				gameObject.GetComponent<SpriteRenderer>().sprite = starCard;
				break;
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
				Debug.Log(obj);
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

			Destroy(gameObject);
		}
	}
}

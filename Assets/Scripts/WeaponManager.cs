using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : EnhancedBehaviour 
{
	public float shootSpeed = 5f;
	public GameObject defaultPrefab;
	public GameObject machineGunPrefab;
	public GameObject shotgunPrefab;
	public GameObject rocketPrefab;
	public int defaultPooledAmmount = 5;
	public int machineGunPooledAmmount = 20;
	public int shotgunPooledAmmount = 20;
	public int rocketPooledAmmount = 5;

	public int ammo;
	private bool fireWeapon;
	private float defaultCooldownTime = 0.3f;
	private float machineGunCooldownTime = 0.1f;
	private float shotgunCooldownTime = 0.4f;
	private float rocketCooldownTime = 1f;
	private Vector2 shootDirection;
	private Transform myTransform;

	private ScoreManager scoreManager;
	public WeaponType weaponType;

	private ScoreManager sm;

	private List<GameObject> defaultBullets;
	private List<GameObject> machineGunBullets;
	private List<GameObject> shotgunBullets;
	private List<GameObject> rocketBullets;

	public enum WeaponType {DEFAULT, MACHINE_GUN, SHOTGUN, ROCKET_LAUNCHER};

	// Use this for initialization
	protected override void EnhancedStart ()
	{
		base.EnhancedStart();
		ammo = 0;
		fireWeapon = true;
		shootDirection = new Vector2(0f, 0f);
		myTransform = transform;
		weaponType = WeaponType.DEFAULT;

		scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

		//Pooling bullets...
		defaultBullets = new List<GameObject>();
		machineGunBullets = new List<GameObject>();
		shotgunBullets = new List<GameObject>();
		rocketBullets = new List<GameObject>();

		GameObject gameScene = GameObject.Find("Game");

		//Default...
		for (int i = 0; i < defaultPooledAmmount; i++)
		{
			GameObject obj = (GameObject)Instantiate(defaultPrefab);
			obj.transform.parent = gameScene.transform;
			obj.SetActive(false);
			obj.GetComponent<SpriteRenderer>().sortingLayerName = "Game";
			defaultBullets.Add(obj);
		}

		//Machine Gun...
		for (int j = 0; j < machineGunPooledAmmount; j++)
		{
			GameObject obj = (GameObject)Instantiate(machineGunPrefab);
			obj.transform.parent = gameScene.transform;
			obj.SetActive(false);
			obj.GetComponent<SpriteRenderer>().sortingLayerName = "Game";
			machineGunBullets.Add(obj);
		}

		//Shotgun...
		for (int k = 0; k < shotgunPooledAmmount; k++)
		{
			GameObject obj = (GameObject)Instantiate(shotgunPrefab);
			obj.transform.parent = gameScene.transform;
			obj.SetActive(false);
			obj.GetComponent<SpriteRenderer>().sortingLayerName = "Game";
			shotgunBullets.Add(obj);
		}

		//And Rockets!
		for (int l = 0; l < rocketPooledAmmount; l++)
		{
			GameObject obj = (GameObject)Instantiate(rocketPrefab);
			obj.transform.parent = gameScene.transform;
			obj.SetActive(false);
			obj.GetComponent<SpriteRenderer>().sortingLayerName = "Game";
			rocketBullets.Add(obj);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		shootDirection = Vector2.zero;

		//getting the shooting input. To be updated once we have dual sticks set up
		if (Input.GetKey(KeyCode.I))
		{
			shootDirection += Vector2.up;
		}
		if (Input.GetKey(KeyCode.K))
		{
			shootDirection -= Vector2.up;
		}
		if (Input.GetKey(KeyCode.J))
		{
			shootDirection -= Vector2.right;
		}
		if (Input.GetKey(KeyCode.L))
		{
			shootDirection += Vector2.right;
		}

		//shooting
		if (shootDirection != Vector2.zero && fireWeapon && GetComponent<HeroManager>().EnableInput)
		{
			GetComponent<HeroManager>().SetBodySprite(shootDirection);
			switch (weaponType)
			{
				case WeaponType.DEFAULT:
					for (int i = 0; i < defaultBullets.Count; i++)
					{
						if (!defaultBullets[i].activeInHierarchy)
						{
							defaultBullets[i].transform.position = myTransform.position + (new Vector3(shootDirection.x, shootDirection.y, 0f) * 0.1f);
							defaultBullets[i].transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0.0f, 360.0f));

							defaultBullets[i].GetComponent<BulletManager>().bulletDirection = shootDirection;
							defaultBullets[i].SetActive(true);
							break;
						}
					}
					break;

				case WeaponType.MACHINE_GUN:
					for (int i = 0; i < machineGunBullets.Count; i++)
					{
						if (!machineGunBullets[i].activeInHierarchy)
						{
							machineGunBullets[i].transform.position = myTransform.position + (new Vector3(shootDirection.x, shootDirection.y, 0f) * 0.5f);
							machineGunBullets[i].GetComponent<BulletManager>().bulletDirection = shootDirection;
							SetBulletRotation(machineGunBullets[i]);
							machineGunBullets[i].SetActive(true);
							ammo--;
							break;
						}
					}
					break;

				case WeaponType.SHOTGUN:
					GameObject bullet1 = null;
					GameObject bullet2 = null;
					//first bullet
					for (int i = 0; i < shotgunBullets.Count; i++)
					{
						if (!shotgunBullets[i].activeInHierarchy)
						{
							shotgunBullets[i].transform.position = myTransform.position + (new Vector3(shootDirection.x, shootDirection.y, 0f) * 0.5f);
							shotgunBullets[i].GetComponent<BulletManager>().bulletDirection = shootDirection;
							shotgunBullets[i].SetActive(true);
							break;
						}
					}

					//second bullet
					for (int j = 0; j < shotgunBullets.Count; j++)
					{
						if (!shotgunBullets[j].activeInHierarchy)
						{
							shotgunBullets[j].transform.position = myTransform.position + (new Vector3(shootDirection.x, shootDirection.y, 0f) * 0.5f);
							shotgunBullets[j].SetActive(true);
							bullet1 = shotgunBullets[j];
							break;
						}
					}

					//third bullet
					for (int k = 0; k < shotgunBullets.Count; k++)
					{
						if (!shotgunBullets[k].activeInHierarchy)
						{
							shotgunBullets[k].transform.position = myTransform.position + (new Vector3(shootDirection.x, shootDirection.y, 0f) * 0.5f);
							shotgunBullets[k].SetActive(true);
							bullet2 = shotgunBullets[k];
							break;
						}
					}
					ammo--;
					SetShotgunBulletsDirections(bullet1, bullet2);
					break;

				case WeaponType.ROCKET_LAUNCHER:
					for (int l = 0; l < rocketBullets.Count; l++)
					{
						if (!rocketBullets[l].activeInHierarchy)
						{
							rocketBullets[l].transform.position = myTransform.position + (new Vector3(shootDirection.x, shootDirection.y, 0f) * 0.5f);
							rocketBullets[l].GetComponent<BulletManager>().bulletDirection = shootDirection;
							SetBulletRotation(rocketBullets[l]);
							rocketBullets[l].SetActive(true);
							break;
						}
					}
					ammo--;
				break;
			}
			if (ammo == 0)
			{
				weaponType = WeaponType.DEFAULT;
			}
			scoreManager.UpdateScore();
			fireWeapon = false;
			StartCoroutine("Cooldown");
		}
	}
	
	IEnumerator Cooldown()
	{
		switch (weaponType)
		{
			case WeaponType.DEFAULT:
				yield return new WaitForSeconds(defaultCooldownTime);
				break;

			case WeaponType.MACHINE_GUN:
				yield return new WaitForSeconds(machineGunCooldownTime);
				break;
	
			case WeaponType.SHOTGUN:
				yield return new WaitForSeconds(shotgunCooldownTime);
				break;

			case WeaponType.ROCKET_LAUNCHER:
				yield return new WaitForSeconds(rocketCooldownTime);
				break;

			default:
				yield return new WaitForSeconds(defaultCooldownTime);
				break;
		}
		fireWeapon = true;
	}

	void SetShotgunBulletsDirections(GameObject b1, GameObject b2)
	{
		if (shootDirection.x > 0f) //right
		{
			if (shootDirection.y > 0f) //up
			{
				b1.GetComponent<BulletManager>().bulletDirection = new Vector2(1.3f, 0.7f);
				b2.GetComponent<BulletManager>().bulletDirection = new Vector2(0.7f, 1.3f);
			}

			else if (shootDirection.y < 0f) //down
			{
				b1.GetComponent<BulletManager>().bulletDirection = new Vector2(1.3f, -0.7f);
				b2.GetComponent<BulletManager>().bulletDirection = new Vector2(0.7f, -1.3f);
			}

			else //center
			{
				b1.GetComponent<BulletManager>().bulletDirection = new Vector2(0.85f, 0.3f);
				b2.GetComponent<BulletManager>().bulletDirection = new Vector2(0.85f, -0.3f);
			}
		}

		else if (shootDirection.x < 0f) //left
		{
			if (shootDirection.y > 0f) //up
			{
				b1.GetComponent<BulletManager>().bulletDirection = new Vector2(-1.3f, 0.7f);
				b2.GetComponent<BulletManager>().bulletDirection = new Vector2(-0.7f, 1.3f);
			}
			
			else if (shootDirection.y < 0f) //down
			{
				b1.GetComponent<BulletManager>().bulletDirection = new Vector2(-1.3f, -0.7f);
				b2.GetComponent<BulletManager>().bulletDirection = new Vector2(-0.7f, -1.3f);
			}
			
			else //center
			{
				b1.GetComponent<BulletManager>().bulletDirection = new Vector2(-0.85f, 0.3f);
				b2.GetComponent<BulletManager>().bulletDirection = new Vector2(-0.85f, -0.3f);
			}
		}

		//shootDirection.x == 0 - center
		else
		{
			if (shootDirection.y > 0f) //up
			{
				b1.GetComponent<BulletManager>().bulletDirection = new Vector2(0.3f, 0.85f);
				b2.GetComponent<BulletManager>().bulletDirection = new Vector2(-0.3f, 0.85f);
			}
			
			else if (shootDirection.y < 0f) //down
			{
				b1.GetComponent<BulletManager>().bulletDirection = new Vector2(0.3f, -0.85f);
				b2.GetComponent<BulletManager>().bulletDirection = new Vector2(-0.3f, -0.85f);
			}
		}
	}

	void SetBulletRotation(GameObject bullet)
	{
		float z = 0.0f;
		if (shootDirection.x > 0) //Right
		{
			if (shootDirection.y > 0) //up
			{
				z = 225f;
			}
			else if (shootDirection.y < 0) //down
			{
				z = 135f;
			}
			else //zero
			{
				z = 180f;
			}
		} 
		else if (shootDirection.x < 0) //Left
		{
			if (shootDirection.y > 0) //up
			{
				z = 315f;
			}
			else if (shootDirection.y < 0) //down
			{
				z = 45f;
			}
			else //zero
			{
				z = 0f;
			}
		}
		else //zero
		{
			if (shootDirection.y > 0) //up
			{
				z = 270f;
			}
			else if (shootDirection.y < 0) //down
			{
				z = 90f;
			}
		}

		bullet.transform.rotation = Quaternion.Euler(0f, 0f, z);
	}
}

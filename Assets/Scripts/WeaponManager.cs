using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour 
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

	private bool fireWeapon;
	private float defaultCooldownTime = 0.5f;
	private float machineGunCooldownTime = 0.1f;
	private float shotgunCooldownTime = 0.5f;
	private float rocketCooldownTime = 1f;
	private Vector2 shootDirection;
	private Transform myTransform;
	private WeaponType weaponType;

	private List<GameObject> defaultBullets;
	private List<GameObject> machineGunBullets;
	private List<GameObject> shotgunBullets;
	private List<GameObject> rocketBullets;

	private enum WeaponType {DEFAULT, MACHINE_GUN, SHOTGUN, ROCKET_LAUNCHER};

	// Use this for initialization
	void Start () 
	{	
		fireWeapon = true;
		shootDirection = new Vector2(0f, 0f);
		myTransform = transform;
		weaponType = WeaponType.SHOTGUN;

		//Pooling bullets...
		defaultBullets = new List<GameObject>();
		machineGunBullets = new List<GameObject>();
		shotgunBullets = new List<GameObject>();
		rocketBullets = new List<GameObject>();

		//Default...
		for (int i = 0; i < defaultPooledAmmount; i++)
		{
			GameObject obj = (GameObject)Instantiate(defaultPrefab);
			obj.SetActive(false);
			defaultBullets.Add(obj);
		}

		//Machine Gun...
		for (int j = 0; j < machineGunPooledAmmount; j++)
		{
			GameObject obj = (GameObject)Instantiate(machineGunPrefab);
			obj.SetActive(false);
			machineGunBullets.Add(obj);
		}

		//Shotgun...
		for (int k = 0; k < shotgunPooledAmmount; k++)
		{
			GameObject obj = (GameObject)Instantiate(shotgunPrefab);
			obj.SetActive(false);
			shotgunBullets.Add(obj);
		}

		//And Rockets!
		for (int l = 0; l < rocketPooledAmmount; l++)
		{
			GameObject obj = (GameObject)Instantiate(rocketPrefab);
			obj.SetActive(false);
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
		if (shootDirection != Vector2.zero && fireWeapon)
		{
			switch (weaponType)
			{
				case WeaponType.DEFAULT:
					for (int i = 0; i < defaultBullets.Count; i++)
					{
						if (!defaultBullets[i].activeInHierarchy)
						{
							defaultBullets[i].transform.position = myTransform.position;
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
							machineGunBullets[i].transform.position = myTransform.position;
							machineGunBullets[i].GetComponent<BulletManager>().bulletDirection = shootDirection;
							machineGunBullets[i].SetActive(true);
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
							shotgunBullets[i].transform.position = myTransform.position;
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
							shotgunBullets[j].transform.position = myTransform.position;
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
							shotgunBullets[k].transform.position = myTransform.position;
							shotgunBullets[k].SetActive(true);
							bullet2 = shotgunBullets[k];
							break;
						}
					}
					//bp1.GetComponent<BulletManager>().bulletDirection = shootDirection;
					SetShotgunBulletsDirections(bullet1, bullet2);
					break;

				case WeaponType.ROCKET_LAUNCHER:
					break;
			}
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
}

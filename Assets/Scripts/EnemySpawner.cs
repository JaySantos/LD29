using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : EnhancedBehaviour {

	public float freezeTime = 5.0f;

	[SerializeField]
	Enemy[] enemyPrefabs;

	/// <summary>
	/// Guarda a posiçao das portas do jogo.
	/// </summary>
	Transform[] spawnPoints;

	/// <summary>
	/// The hero.
	/// </summary>
	[SerializeField]
	Transform player;

	/// <summary>
	/// The timer spawn.
	/// </summary>
	Timer timerSpawn;

	/// <summary>
	/// The spawn interval.
	/// </summary>
	float spawnInterval = 1f;

	bool isFrozen;
	GameObject gameScene;
	
	public bool IsFrozen {
		get {
			return isFrozen;
		}
		set {
			isFrozen = value;
			if (isFrozen)
			{
				StartCoroutine("Unfreeze");
			}
		}
	}

	protected override void EnhancedStart ()
	{
		base.EnhancedStart ();
	}

	protected override void EnhancedOnEnable()
	{
		base.EnhancedOnEnable();

		// Crio uma piscina de inimigos para reaproveitar
<<<<<<< HEAD
		enemyPrefab.CreatePool();
		
		List<Transform> transforms = new List<Transform>();
		foreach (Transform t in GetComponentsInChildren<Transform>(false))
		{
			if (t.gameObject.tag != "StartPoint")
			{
				transforms.Add (t);
			}
		}
=======
		for (int i = 0; i < enemyPrefabs.Length; i++) {
			enemyPrefabs[i].CreatePool();	
		}

		List<Transform> transforms = new List<Transform>(GetComponentsInChildren<Transform>(false));
>>>>>>> FETCH_HEAD
		transforms.Remove(transform);
		spawnPoints = transforms.ToArray();
		gameScene = GameObject.Find("Game");
	}

	[SerializeField]
	int maxEnemies = 50;

	public int MaxEnemies
	{
		get
		{
			return maxEnemies;
		}
		set
		{
			maxEnemies = value;
		}
	}

	public static int NumEnemies = 0;

	public void StartSpawning()
	{
		InvokeRepeating("Spawn", 3f, spawnInterval);
	}

	public void StopSpawning()
	{
		CancelInvoke();
	}

	void Spawn() {
<<<<<<< HEAD
		if(NumEnemies < maxEnemies && !IsFrozen) 
		{
			Transform chosenPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
			Transform endPoint = null;
			foreach (Transform t in chosenPoint)
			{
				if (t.gameObject.tag == "StartPoint")
				{
					endPoint = t;
				}
			}
			Vector3 rndPosition = chosenPoint.position;
			Enemy enemy = enemyPrefab.Spawn(rndPosition);
			enemy.startPoint = rndPosition;
			enemy.endPoint = endPoint.position;
=======

		if(NumEnemies < maxEnemies && !IsFrozen) {

			Vector3 rndPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
			Enemy enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)].Spawn(rndPosition);
>>>>>>> FETCH_HEAD
			enemy.gameObject.transform.parent = gameScene.transform;
			enemy.GetComponent<SpriteRenderer>().sortingLayerID = 2;
			enemy.Player = player;

			NumEnemies++;
		}
	}

	IEnumerator Unfreeze()
	{
		yield return new WaitForSeconds(freezeTime);
		IsFrozen = false;
	}
}

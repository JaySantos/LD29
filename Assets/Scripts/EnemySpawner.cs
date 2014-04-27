using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : EnhancedBehaviour {

	public float freezeTime = 5.0f;

	[SerializeField]
	Enemy enemyPrefab;

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
	float spawnInterval = 2f;

	bool isFrozen;
	
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

		// Crio uma piscina de inimigos para reaproveitar
		enemyPrefab.CreatePool();

		List<Transform> transforms = new List<Transform>(GetComponentsInChildren<Transform>(false));
		transforms.Remove(transform);
		spawnPoints = transforms.ToArray();
		InvokeRepeating("Spawn", Time.time, spawnInterval);
	}

	[SerializeField]
	int maxEnemies = 5;

	public static int NumEnemies = 0;

	void Spawn() {

		if(NumEnemies < maxEnemies && !IsFrozen) {

			Vector3 rndPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
			Enemy enemy = enemyPrefab.Spawn(rndPosition);
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

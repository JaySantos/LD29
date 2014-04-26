using UnityEngine;
using System.Collections;

public class EnemySpawner : EnhancedBehaviour {

	[SerializeField]
	Enemy enemyPrefab;

	/// <summary>
	/// Guarda a posiçao das portas do jogo.
	/// </summary>
	[SerializeField]
	Transform[] doors;

	/// <summary>
	/// The hero.
	/// </summary>
	[SerializeField]
	Transform hero;

	/// <summary>
	/// The timer spawn.
	/// </summary>
	Timer timerSpawn;

	/// <summary>
	/// The spawn interval.
	/// </summary>
	float spawnInterval = 2f;

	protected override void EnhancedStart ()
	{
		base.EnhancedStart ();

		// Crio uma piscina de inimigos para reaproveitar
		enemyPrefab.CreatePool();
		InvokeRepeating("Spawn", Time.time, spawnInterval);
	}
	
	void Spawn() {
		Vector3 rndPosition = doors[Random.Range(0, doors.Length)].position;
		Enemy enemy = enemyPrefab.Spawn(rndPosition);
		enemy.Hero = hero;
	}
}

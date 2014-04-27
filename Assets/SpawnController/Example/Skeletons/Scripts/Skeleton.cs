using UnityEngine;
using System.Collections;

public class Skeleton : MonoBehaviour
{
	public float speed = 1f;
	public int health = 1;
	public int rotationIntervalMin = 2;
	public int rotationIntervalMax = 5;

	private SpawnedEnemy spawnedEnemy;
	private Transform trans;

	// Use this for initialization
	void Start()
	{
		animation["run"].wrapMode = WrapMode.Loop;
		trans = transform;
		spawnedEnemy = gameObject.GetComponent("SpawnedEnemy") as SpawnedEnemy; // get the component into a variable
		Invoke("RandRotate", Random.Range(rotationIntervalMin, rotationIntervalMax));
	}

	// Update is called once per frame
	void Update()
	{
		trans.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	void OnMouseUp()
	{
		health--;

		if ( health <= 0 )
			spawnedEnemy.Dead(); // this needs to be called, or our Waves/Spawns that use the 'Kill' Type will not behave properly
	}

	void OnSpawn()
	{
		health = 1;
		speed = 1f;
	}

	void RandRotate()
	{
		trans.Rotate(Vector3.up * Random.Range(0f, 360f));
		Invoke("RandRotate", Random.Range(rotationIntervalMin, rotationIntervalMax));
	}

	// we are demostrating the use of SendMessages from within the SpawnController :)
	void SetSpeed(string sp)
	{
		speed = System.Int32.Parse(sp); // it does get passed as a String :(
	}

	void SetHealth(string he)
	{
		health = System.Int32.Parse(he);
	}
	void SetRotationMin(string m)
	{
		rotationIntervalMin = System.Int32.Parse(m);
	}
	void SetRotationMax(string m)
	{
		rotationIntervalMax = System.Int32.Parse(m);
	}
}
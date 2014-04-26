using UnityEngine;
using System.Collections;

public class SpherePowerUpTest : MonoBehaviour 
{
	//public GameObject spherePowerUp;
	//public GameObject sphere;
	public float xSpeed = 5f;
	public float rotationSpeed = 100f;
	public float timeToExplode = 2f;

	private Transform myTransform;

	// Use this for initialization
	void Start () 
	{
		myTransform = transform;

		StartCoroutine("Explode");
	}

	void Update ()
	{
		myTransform.Rotate(0f,0f,Time.deltaTime*rotationSpeed);
	}

	IEnumerator Explode()
	{
		yield return new WaitForSeconds(timeToExplode);
		rotationSpeed = 0f;
		foreach (Transform t in transform)
		{
			t.GetComponent<SphereManager>().moveSphere = true;
			t.GetComponent<Animation>().Stop();
			Destroy(t.gameObject, 2.0f);
		}
	}
}

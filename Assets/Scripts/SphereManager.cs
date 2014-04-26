using UnityEngine;
using System.Collections;

public class SphereManager : MonoBehaviour 
{
	public bool moveSphere = false;
	public float speed = 10f;

	private Transform myTransform;
	// Use this for initialization
	void Start () 
	{
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (moveSphere)
		{
			myTransform.Translate(new Vector2(speed * Time.deltaTime, 0f));
		}
	}
}

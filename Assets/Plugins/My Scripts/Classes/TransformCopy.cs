using UnityEngine;

public class TransformCopy {
	
	public Vector3 Position 
	{
		get;
		set;
	}
	
	public Quaternion Rotation 
	{
		get;
		set;
	}
	
	public Vector3 LocalScale
	{
		get;
		set;
	}
	
	public TransformCopy(Vector3 position, Quaternion rotation, Vector3 localScale) {
		Position = position;
		Rotation = rotation;
		LocalScale = localScale;
	}
}
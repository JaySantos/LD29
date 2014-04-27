//
// CameraShakeEditor.cs
//
// Author(s):
//       Josh Montoute <josh@thinksquirrel.com>
//
// Copyright (c) 2012-2014 Thinksquirrel Software, LLC
//
using UnityEngine;
using UnityEditor;
#if !(UNITY_3_3 || UNITY_3_4 || UNITY_3_5)
using Thinksquirrel.Utilities;
#endif

#if !(UNITY_3_3 || UNITY_3_4 || UNITY_3_5)
namespace Thinksquirrel.UtilitiesEditor
{
#endif
	#if !UNITY_3_3 && !UNITY_3_4
	[CanEditMultipleObjects]
	#endif
	[CustomEditor(typeof(CameraShake))]
	public class CameraShakeInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			GUILayout.Label("Camera Shake v. 1.3.1f1");
			DrawDefaultInspector();
		}
	}
#if !(UNITY_3_3 || UNITY_3_4 || UNITY_3_5)
}
#endif
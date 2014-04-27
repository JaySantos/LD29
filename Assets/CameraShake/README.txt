Camera Shake
Copyright 2012-2014 Thinksquirrel Software, LLC.

Camera Shake is an easy to use solution for procedural camera shake animation. 
It has one easy-to-use component! Camera Shake can also shake the GUI as well.
Advanced features include multiple shakes at the same time and asynchronous callbacks.

Usage:
	1) Put the CameraShake component on a camera, or any game object (Component > Utilities > Camera Shake).
	
	2) From any of your scripts, call CameraShake.ShakeAll(). You can also call CameraShake.Shake on any individual Camera Shake Component
	
	3) If you need to shake anything in OnGUI, use the following methods on the CameraShake object at the start and end of your OnGUI callback:
		
		BeginShakeGUI()
		EndShakeGUI()
		BeginShakeGUILayout()
		EndShakeGUILayout()
		
		The CameraShakeExample script shows how this works.
		
	4) For advanced users:
		
		Multiple Camera Shake components are supported.
		Callbacks are supported for each camera shake.
		There are also events in the CameraShake class for when a camera starts shaking and when it completely finishes shaking, as well as before and after each shake event.
		Check the CameraShake class for a list of all available properties and methods.

UnityScript note:
	To use CameraShake with UnityScript, move the Assets/CameraShake folder to the Assets/Plugins folder, and Assets/CameraShake/Editor to Assets/Editor.
		
Inspector property overview:

Cameras	- The cameras to shake. This will default to a camera component on the same game object. If none is present it will try to find the main camera.
Shake Type - The type of shake to perform (camera matrix or local position). Defaults to shaking the camera matrix.
Number Of Shakes - The maximum number of shakes to perform.
Shake Amount - The amount to shake in each direction.
Rotation Amount - The amount to rotate in each axis.
Distance - The initial distance for the first shake.
Speed - The speed multiplier for the shake.
Decay - The decay speed (between 0 and 1). Higher values will stop shaking sooner.
Gui Shake Modifier - The modifier applied to speed in order to shake the GUI. NOTE: The GUI shake distance is based on the first camera in the camera list.
Multiply By Time Scale - If true, multiplies the shake speed by the time scale.

If you have any questions or feedback, feel free to contact Thinksquirrel at:
https://thinksquirrel.com
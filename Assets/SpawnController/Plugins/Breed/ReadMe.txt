-------------------------------
Breed
Copyright 2011-2012 big G games
Version 1.3
http://www.big-g-games.com
gregory@big-g-games.com
-------------------------------

Thank you for buying Breed!

If you have any questions, suggestions, comments or feature requests, please don't hesitate to email gregory@big-g-games.com

I PLEASE ask that rate the plugin in the Unity3D Asset Store (or send me an email telling me your rate 1 (horrible) through 5 (great), haha)

-----
Setup
-----

// creating a breed, it must be given a name and a game object
Breed.Instance().Create("bullets", bulletPrefab);

// spawn the bullet - use this instead of "Instantiate"
Breed.Instance().Get("bullets").Spawn();

// unspawn the bullet - use this instead of "Destroy"
Breed.Instance().GetPool("bullets").Unspawn(gameObject);

For more info, look at the Documentation

-------------
Documentation
-------------

Can be found here: http://www.big-g-games.com/plugins/docs

---------------
Version History
---------------

1.3
	- Added a c# version :D
	- Some minor bug fixes
1.2
	- Added "RemoveAll" - thanks to Chris for find
1.1
	- Renamed "Pool Manager" to "Breed"
1.0.2
	- Clean up "Setup" in the "ReadMe.txt" file
1.0.1
	- Forgot to add "SendMessageOptions.DontRequireReceiver" to the "OnUnspawn" method
1.0
	- Initial Launch
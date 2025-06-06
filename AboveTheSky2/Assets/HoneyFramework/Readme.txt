 Honey Hex Framework
ver. 1.5.5 July 2017

Created by MuHa Games - Robert 'Khash' Aguero Padilla & Ewa 'A'vee' Aguero Padilla
A* Pathfinding Project used and distributed with consent of its author Aron Granberg (http://arongranberg.com/astar/)
Uses modified UFT Atlas Editor (http://forum.unity3d.com/threads/atlas-editor.159893/)

Support forum: http://honey.boards.net/

Thank you for purchasing Honey! 
For quick start guide please refer to "Tutorial Basic.pdf" located in HoneyFramework root folder.
Tutorial videos can be found here:

Tutorial 1 - Quick Start 
https://www.youtube.com/watch?v=jrOk3qJSrCg

Tutorial 2 - Setting up foreground atlas and terrain definitions
https://www.youtube.com/watch?v=TQmIoBXYSx0


If you publish a game that uses Honey we would really love to see it! Write to us and let us know!


Changelog:
v. 1.5.5
- updated A* Pathfinding in Unity 5.6.1 release - many thanks to Aron Granberg!

v. 1.5.4
- fixed compatibility with Unity 5.6.1


v. 1.5.3
- fixed compatibility with Unity 5.5


v. 1.5.2
New feature:
- markers for non-DX11 mode (for all of our customers who use honey on Apple, Androids and other devices).


v. 1.5.1
- Fixing errors related to pathfinding


v. 1.5
New Features:
- Better chunk rebuild, with less visible artifacts (swap only after new data is ready)
- Improved and extended markers:
  * rotation, 
  * atlas supports 8 times more graphics, 
  * space for 8 floating point slots in texture space
- Roads - small system to use markers to place roads on the world, it can be easily changed to drawing borders instead with a bit of coding 
- Updated A* Pathfinding to 3.6.1 (2015-04-06)


v. 1.3.1
- Checked compatibility with Unity 5 Personal and submitted using Unity 5 
Fix:
- Unity 5 serialization fix


v 1.3.0
New Features:
- Basic fog of War system
- Hex visibility status

Fix:
- Extended comments in river factory class


v 1.2.7
Fix:
- Water gets custom level on all non Windows platforms instead of Android as it was before. This is to counter the lack of the transparency (incorrect behavior of the transparency) in OpenGl.


v 1.2.6 
New Features:
- Display DX mode of honey (assets of which mode are in use)
- River Neighbors: Hexes know if they are next to the river and which hexes around them need to be accessed by passing the river
- New button on the camera to mark all hexes next to a river
- Two helper functions in HexMarkers (thanks to Lennartos)

Fix:
- Memory issue in HexGrid corrected (thanks to Lennartos)
- Some descriptions, names and other strings to clarify code transparency
- Marker texture does not use MipMaps anymore. This feature was producing artifacts on hex borders


v.1.2.5
Non DX11 mesh:
- Improved memory footprint
- Vertexes contain averaged normals which produces effects similar to smooth groups - for those using real time lightening. 

To those which seek a challenge - we are providing you with (still a work-in-progress) a different approach in the way we plan to further develop Honey.
In "Honey 2 WIP" folder you can find a package with Honey2. Import this package in a new project.
This new system will aim to not require Unity Pro, and shrink memory footprint to fraction of what was required by Honey1.
Advanced shader does realtime blending and is capable of mimicking many of the Honey1 features (but not all!) 
It is able to produce world of the size of 10000+ hexes with little to no extra memory when compared with a small (few hexes in size) world. 

We would like to assure that Honey2 would NOT be sold separately and will be always in the same package as Honey1 (potentially replacing it when it is done), but none of the original owners would be required to buy it again.

v.1.2
- Improved memory management
- Fixed memory leaks from UnityEngine.RenderTexture.GetTemporary by... not using this system, and writing our own in its place.
- Ability to rebuild/update chunks at runtime (Hit Generate World button again during runtime)
- Ability to update, add or remove foreground objects (e.g. trees) in a specified area (e.g. circle). Look for EXAMPLE1 in CameraControler class (Note: You can manipulate each tree independently or update tree clusters).
- Ability to place hex markers (e.g. unit ownershp: green - friendly, red - enemy... etc.). Look for EXAMPLE2 in CameraControler class.
- Ability to change/update a single hex at runtime. Look for EXAMPLE3 in CameraControler class.
- Ability to save chunk textures to drive
- Ability to save world data to drive (terrain and foreground). It can be extended if you prefer to save more data!
- Ability to load world data and regenerate the whole world from it.
Note: in the future I plan to mix texture saves with datasaves and be able to rebuild data with no need to bake it again (fast load) but I'm still trying to work out how to shrink save size of all those textures.


v 1.1
- new fork rivers (merging rivers system)
- river direction preference
- sprite pivot for foreground instead of auto-centering
- Number of fixes in river system
- code comments fixes and clarifications
- mountain height texture adjustment

v.1.0
- initial release


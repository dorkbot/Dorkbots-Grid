# Dorkbots-Grid
A framework for managing objects on a grid. Grid cell objects store a state and type fields and include events to facilitate implementation of an observer pattern.

There are two grid frameworks -

The Grid is the base framework, it's best suited for building 2D games like tik tac toe, checkers, chess, or matching games, etc.

The WorldGrid framework is built on top of the Grid framework, it deals with managing Entities. Entities could be enemies and players, or other interactive objects. The WorldGrid keeps track of where Entities are located and breaks up a plane into grid coordinates. When dealing with lots of entities, you can get a performance boost by not searching the entire scene/world for entities and instead look at entities in adjacent cells in the grid.

http://www.dorkbots.com/repositories/grid/index.html

Author: Dayvid jones<br>
http://www.dayvid.com<br>
Copyright (c) Superhero Robot 2017<br>
http://www.superherorobot.com


Example -

There is an example project, the example uses the WorldGrid framework and the Dorkbots Broadcasters.

Look at this file -<br>
https://github.com/dorkbot/Dorkbots-Grid/blob/master/Example/Assets/Scripts/Game/Controller.cs
Look at the Start method, there you will see the Grid being set up. You will also find an example of the Dorkbots Broadcasters being used. The Update method shows how the Moving Entities are updated. And the Event Handler PlayerMovedEvent method shows how to find entities that are within a certain distance and then starting interactions with those entities.

Look at this file -<br>
https://github.com/dorkbot/Dorkbots-Grid/blob/master/Example/Assets/Scenes/Game.unity
In this scene notice how all of the entities are placed in one Parent Game Object called "World". Look at the Game Objects "NPC", "Player", "Sphere" and "Hangar/DoorPanel", check out their attached Scripts, they all have to have a "GridCellEntity" Script attached so they can be added to the grid. They also have game specific scripts that attach event handlers to the GridCellEntity Scripts.

You can find the Dorkbots Broadcaster framework here -<br> http://dorkbots.com/repositories/broadcasters/index.html
# Artanis 2D Engine

This Github repository contains my own Monogame 2D Engine.

## Getting Started

In order to use this on your project, you need to either add the DLL as reference or add the whole solution inside your monogame project. In both cases you need to use the correct using statement in each file:
```
using Arta2DEngine;
```

### Prerequisites

This engine uses Monogame 3.7.1. It might work on other versions, but it is not tested against any other than 3.7.1.

### Installing

Simply clone this repository inside a folder of your choice with:

```
git clone https://github.com/Artanisx/Arta2DEngine.git
```

Then add the project to your solution, then add it (Arta2DEngine) as a reference to your main game project. You can find it in the "Projects" category of the Reference Manager window.
![Adding Arta2DEngine as reference](http://puu.sh/F1tbT/1982549470.png)

If you prefer to go with the DLL installation (so you don't need access to the Engine source code), you don't need to clone the project, you only need to download the latest release version of the DLL, put it inside your project folder and add it as a Reference, looking into the "Browse" category of your Reference Manager window.

### Documentation

You can find the documentation clicking this [link](Documentation/userguide.md).

### Tutorials

You can find the tutorials clicking this [link](Documentation/tutorials.md).

### Features

This engine has several basic features, like:

* Game Object system, to deal with object like entities
* 2D sprite functionality, including basic animation from texture atlases
* Audio support for sound effects and music
* A basic 2D camera system with pan, zoom and lookat functionality
* A basic 2d primitives system to help with debugging and for collision checks
* A basic system to handle collisions, thanks to automatically generated bounding boxes and circles using the Intersects method (and Contains for circles).
* A basic screen manager system to handle multiple screens, like menu, gameplay and outro screens
* A basic customizable Particle Engine system, with easily extendable effects
* A basic UI system with buttons with easily generated events
* A basic Input system built around a "command" object that can work with keyboard, mouse and gamepad

## Built With

* [Monogame](https://www.monogame.net/) - Monogame Engine

## Authors

* **Fabrizio  Tobia** - [Artanisx](https://github.com/Artanisx/)

## Extra Credits
During the development of Arta2DEngine I've studied tutorials and source codes from several different sources. Below the list of the ones I specifically recall at the time of writing:

* [RB Whitaker's Wiki](http://rbwhitaker.wikidot.com/monogame-tutorials)
* [SimonDarksideJ / XNAGameStudio Repo](https://github.com/SimonDarksideJ/XNAGameStudio)
* [C3 2D XNA Primitives](https://bitbucket.org/C3/2d-xna-primitives/wiki/Home)
* [XNA Camera 2D](http://www.david-amador.com/2009/10/xna-camera-2d-with-zoom-and-rotation/e)

If I forgot any contribution for the Extra Credits section, please send me a message!

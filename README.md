# Artanis 2D Engine

This private repository contains my own Monogame 2D Engine.

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

You can find the documentation clicking this [link](../Documentation/userguide.md).

## Built With

* [Monogame](https://www.monogame.net/) - Monogame Engine

## Authors

* **Fabrizio  Tobia** - [Artanisx](https://github.com/Artanisx/)
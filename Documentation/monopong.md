# MonoPong

This tutorial will guide you into making a Pong clone with Arta2D Engine! Before starting, have a look at how the full game will look like:
![Main Menu](../images/pong_mainmenu.png)
![Game screen](../images/pong_gamescreen.png)
![Game screen 2](../images/pong_gamescreen_2.png)

## Create the Project
The first thing to do is to have Monogame properly installed. Arta2D Engine uses the Monogame framework and thus it needs it to be correctly installed. Also, you will need the Templates for your Visual Studio version (or what you will be working with). I'm assuming the above is already set, so you can choose the "Monogame Windows Project" template, which is a Monogame for the Windows Desktop platform using DirectX.

After you created the project, you need to add the reference to the Arta2DEngine. To do so, right click on the "References" item under your project and click "Add Reference". 
If you have downloaded the release DLL (or compiled one yourself), you will need to click the Browse button and select the DLL (which you also will need to distribute in your compiled game).
If you want to have the source of the engine available to you, then you need to git clone the Arta2DEngine and add this project to your solution. At that point, you will find the "Arta2DEngine" reference in the Projects tab. Select it and click OK.

Go back to the [Readme](../README.md) for Getting Started.

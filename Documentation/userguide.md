# Arta2D Engine - User Guide 

This User Guide explains how the engine work and how to use it for your games.

### Getting Started

Check the [Readme](../README.md) for Getting Started.

## Classes
### GameObject

This is the first simple class for the Engine. It serves as a container for GameObjects in a similar fashion to Unity 3D engine.
TODO: Complete the guide

To create a GameObject simply do the below:
```c
// Create a GameObjectg
GameObject gameObject;

// Initialize it - LoadContent() is a good place for this
gameObject = new GameObject(goTexture, Vector2.Zero);

// Draw it - Draw() is the perfect place. This must be called inside the Begin / End spriteBatch.Draw.
spriteBatch.Begin();
gameObject.Draw(spriteBatch);
spriteBatch.End();
```

Go back to the [Readme](../README.md).

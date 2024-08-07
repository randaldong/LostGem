# Pixel Sidescroller

## 2D Pixel Specific Settings

- Main Camera --> Rendering --> Anti-aliasing --> No Anti-aliasing

## 1. Assets Loading & Setting

### Tilemap

#### Traditional Tiles

- import & change settings of 2D textures/sprites

  - Texture Type: Sprite
  - Sprite Mode: Single/Multiple

  - Pixels Per Unit: 16/32/64, according to actual texture

  - Filter Mode: Point (using no mipmap, usually for pixel style)

  - Compression: None

- if the mode is multiple, slice the sprite in Sprite Editor: 

  - for character, grid by cell count, fill in C & R, pivot: bottom (so that computations will always be relative to the feet)
  - for environment assets, grid by cell size, fill in pixel size, pivot: center

- create a Tilemap in the scene (a tilemap is like a layer)

- use Tile Palette (Window --> 2D --> Tile Palette) to draw tilemaps
	- create a palette (a collection of tiles that you can choose from, stored as an object in your project, put them inside `Assets/Palettes`)
	- drag the parent texture into the Tile Palette window (this will trigger Unity to ask you to create Tile asset for each sprite, store them in `Assets/Tiles/XXX`). Note, if you drag the children, they will be shuffled and messy in the window.
	- paint the base tilemap using those handy tools (`shift` + click to erase)
	- create a new tilemap (new layer) for overlay levels/objects, set the `Order in Layer` to a greater value, in Tile Palette switch active tilemap

- change the viewport size in game mode to preview; change the camera size to change the scene size

#### Rule Tiles



#### Animated Tiles



### Character

- to put it in front of everything, a good practice is to create different sorting layers for different hierarchies, and put the character in a "bigger" layer
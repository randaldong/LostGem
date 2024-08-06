# Pixel Sidescroller

## 1. Assets Loading & Setting

2D texture/sprites:

- Sprite Mode: Single/Multiple
  - if the mode is multiple, in Sprite Editor, slice the sprite: 
    - for character, grid by cell count, fill in C & R, pivot: bottom (so that computations will always be relative to the feet)
    - for environment assets, grid by cell size, pivot: center; Window –> 2D –> Tile Palette; create tilemap in hierarchy
- Pixels Per Unit: 16/32/64, according to actual texture
- Filter Mode: Point (using no mipmap, usually for pixel style)
- Compression: None
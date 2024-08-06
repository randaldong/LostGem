# Pixel Sidescroller

## 1. Assets Loading & Processing

2D texture/sprites:

- Sprite Mode: Single/Multiple
  - if the mode is multiple, in Sprite Editor, slice the sprite: grid by cell count, fill in C & R, pivot: bottom (so that computations will always be relative to the feet)
- Pixels Per Unit: 16/32/64, according to actual texture
- Filter Mode: Point (using no mipmap, usually for pixel style)
- Compression: None
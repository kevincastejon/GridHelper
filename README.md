# GridHelper

This package offers a utilitary API to help with operations on **grids** such as tile **extraction**, **raycasting**, and **pathfinding**.

Comes with several demo examples.

[See online documentation](https://kevincastejon.github.io/Unity-GridHelper/)

[Get the Unity package](https://github.com/kevincastejon/Unity-GridHelper/releases/latest)

[See my other Unity packages](https://assetstore.unity.com/publishers/46935)

**[Complete API Documentation](https://kevincastejon.fr/demos/Documentations/Unity-GridHelper/)**

## Usages

All you need to use this API is a two-dimensional array of tiles using *row major order* (first index is line, second is the column).

What is a *tile* ? Any object (custom class, struct, component, ...) that implements the very light **ITile** interface of this library. This interface requires four properties getters:
- *bool* **IsWalkable** . Must return if the tile can be walk/see throught (for pathfinding/raycasting)
- *float* **Weight** . Must return the tile's weight (the 'cost' it will takes to walk throught that tile). Minimum is 1.
- *int* **X** . Must return the horizontal position of the tile into the grid
- *int* **Y** . Must return the vertical position of the tile into the grid

This API is using a namespace so you have to add a using instruction to the scripts that will need this library.
```cs
using KevinCastejon.GridHelper;
```

### - <u>Extractions</u>

You can extract tiles in a radius, or in a rectangle, around a tile.

- **GetTilesInARectangle**
```cs
YourCustomTileType[] tiles = Extraction.GetTilesInARectangle(grid, centerTile, rectangleSize);
```
- **GetTilesInARadius**
```cs
YourCustomTileType[] tiles = Extraction.GetTilesInARadius(grid, centerTile, radius);
```

You can also get only the walkable tiles in a radius/rectangle, around a tile.

- **GetWalkableTilesInARectangle**
```cs
YourCustomTileType[] tiles = Extraction.GetWalkableTilesInARectangle(grid, centerTile, rectangleSize);
```
- **GetWalkableTilesInARadius**
```cs
YourCustomTileType[] tiles = Extraction.GetWalkableTilesInARadius(grid, centerTile, radius);
```

You can also get only the tiles on the radius/rectangle outline.

- **GetTilesOnARectangleOutline**
```cs
YourCustomTileType[] tiles = Extraction.GetTilesOnARectangleOutline(grid, centerTile, rectangleSize);
```
- **GetTilesOnARadiusOutline**
```cs
YourCustomTileType[] tiles = Extraction.GetTilesOnARadiusOutline(grid, centerTile, radius);
```

Finally, you can also get only the walkable tiles on the radius/rectangle outline.

- **GetWalkableTilesOnARectangleOutline**
```cs
YourCustomTileType[] tiles = Extraction.GetWalkableTilesOnARectangleOutline(grid, centerTile, rectangleSize);
```
- **GetWalkableTilesOnARadiusOutline**
```cs
YourCustomTileType[] tiles = Extraction.GetWalkableTilesOnARadiusOutline(grid, centerTile, radius);
```

Besides from extracting tiles from shapes, you can know if a specific tile is contained into a radius/rectangle or not. Same with the outlines.

- **IsTileInARectangle**
```cs
bool isTileInARectangle = Extraction.IsTileInARectangle(centerTile, rectangleSize);
```
- **IsTileInARectangleOutline**
```cs
bool isTileInARectangleOutline = Extraction.IsTileInARectangleOutline(centerTile, radius);
```
- **IsTileInARectangle**
```cs
bool isTileInARectangle = Extraction.IsTileInARectangle(centerTile, rectangleSize);
```
- **IsTileInARectangleOutline**
```cs
bool isTileInARectangleOutline = Extraction.IsTileInARectangleOutline(centerTile, radius);
```

### - <u>Raycasting</u>

You can get all the tiles on a line between two tiles

- **GetTilesOnALine**
```cs
YourCustomTileType[] tiles = Raycasting.GetTilesOnALine(grid, startTile, destinationTile);
```

You can also get only the walkable tiles on a line between two tiles

- **GetWalkableTilesOnALine**
```cs
YourCustomTileType[] tiles = Raycasting.GetWalkableTilesOnALine(grid, startTile, destinationTile);
```

You can get the line of sight between two tiles (a line that "stops" at the first encountered unwalkable tile)

- **GetLineOfSight**
```cs
YourCustomTileType[] tiles = Raycasting.GetLineOfSight(grid, startTile, destinationTile);
```

You can know if the line of sight between two tiles is clear (has not encountered any unwalkable tile)

- **IsLineOfSightClear**
```cs
bool isLineClear = Raycasting.IsLineOfSightClear(grid, startTile, destinationTile);
```

### - <u>Pathfinding</u>

The pathfinding part of this library generates and uses a **PathMap** object that holds all the calculated paths data for the entire grid.

This way of doing pathfinding is usefull for some usages (like Tower Defenses) because it calculates once all the paths between one tile, called the "target", and all the others. (The **PathMap** generation uses **Dijkstra** algorithm)

To generate the **PathMap** object, use the **GeneratePathMap** method. You can use an optional *maxDistance* parameter that limits the paths calculation to an amount of distance (movement 'cost'). Default is 0 and means no limit (all accessible tiles on the entire grid will be treated).

```cs
PathMap<YourCustomTileType> pathMap = Pathfinding.GeneratePathMap(grid, targetTile, maxDistance);
```

### - <u>PathMap</u>

Once the **PathMap** object is generated, you can use its several and almost "*cost free*" methods and properties.

You can retrieve the tile that has been used as the target to generate this **PathMap**

- **Target**
```cs
YourCustomTileType tile = pathMap.Target;
```

You can retrieve the *maxDistance* parameter value that has been used to generate this **PathMap**

- **MaxDistance**
```cs
float maxDistance = pathMap.MaxDistance;
```

You can get all the accessible tiles from the target tile.

- **GetAccessibleTiles**
```cs
YourCustomTileType[] tiles = GridHelper.GetAccessibleTiles();
```

You can know if a tile is contained into the **PathMap**. This is usefull before calling the **PathMap** methods that takes a tile as parameter, as this tile has to be contained the **PathMap**.

- **IsTileIntoPathMap**
```cs
bool isTileIntoPathMap = pathMap.IsTileIntoPathMap(tile);
```

You can get all the tiles on the path from a tile to the target.

- **GetPathToTarget**
```cs
YourCustomTileType[] tiles = pathMap.GetPathToTarget(startTile);
```

Or you can get all the tiles on the path from the target to a tile.

- **GetPathFromTarget**
```cs
YourCustomTileType[] tiles = pathMap.GetPathFromTarget(destinationTile);
```

### - <u>PathMap</u> - other features

You can get info on a specific tile through some **PathMap** methods.

Get the distance to the target from a tile.

- **GetDistanceToTargetFromTile**
```cs
float cost = pathMap.GetDistanceToTargetFromTile(tile);
```

You can get the next tile on the path between the target and a tile.

- **GetNextTileFromTile**
```cs
YourCustomTileType nextTile = pathMap.GetNextTileFromTile(tile);
```

You can get the next tile direction on the path between the target and a tile (in 2D grid coordinates).

- **GetNextTileDirectionFromTile**
```cs
Vector2 nextTileDirection = pathMap.GetNextTileDirectionFromTile(tile);
```

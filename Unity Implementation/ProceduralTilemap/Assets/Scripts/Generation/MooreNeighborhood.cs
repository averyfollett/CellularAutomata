// This code file uses a script sourced from https://blogs.unity3d.com/2018/06/07/procedural-patterns-to-use-with-tilemaps-part-ii/
//  Comments and slight alterations have been made by Avery Follett, 12-06-2019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooreNeighborhood : MonoBehaviour
{
    // This function will return a int which has the number of surrounding walls for the given cell
    // An example of how this will look for a whole tilemap:
    // 1 1 1 1 1       2 4 3 4 2
    // 1 0 1 0 1       3 7 4 7 3
    // 1 0 1 0 1   =   2 6 2 6 2
    // 1 0 1 0 1       3 7 4 7 3
    // 1 1 1 1 1       2 4 3 4 2
    static int GetMooreSurroundingTiles(int[,] map, int x, int y, bool edgesAreWalls)
    {
        // We start off with no surrounding tiles
        int tileCount = 0;

        // We check the 8 surrounding tiles
        // X X X
        // X   X
        // X X X
        for (int neighbourX = x - 1; neighbourX <= x + 1; neighbourX++)
        {
            for (int neighbourY = y - 1; neighbourY <= y + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < map.GetUpperBound(0) && neighbourY >= 0 && neighbourY < map.GetUpperBound(1))
                {
                    // We don't want to count the tile we are checking the surroundings of
                    if (neighbourX != x || neighbourY != y)
                    {
                        // If the map says the neighbor is a wall, we add 1 to the tile count
                        tileCount += map[neighbourX, neighbourY];
                    }
                }
            }
        }
        // We are done searching the neighbors so we return the final count
        // Somewhere between 0-8
        return tileCount;
    }



    // This is a smoothing function which runs in tandem with the previous one
    // We give it a number of times to smooth and our current 2D tilemap
    // It uses the previous function to get the number of surrounding tiles from a cell 
    //  and decides whether the current cell should be a wall or not
    // We give it a threshold to help it decide
    public static int[,] SmoothMooreCellularAutomata(int[,] map, bool edgesAreWalls, int smoothCount, int threshold)
    {
        // This loops everything depending on the number of times we choose to smooth
        for (int i = 0; i < smoothCount; i++)
        {
            // For each cell...
            for (int x = 0; x < map.GetUpperBound(0); x++)
            {
                for (int y = 0; y < map.GetUpperBound(1); y++)
                {
                    // We get the number of surrounding tiles
                    int surroundingTiles = GetMooreSurroundingTiles(map, x, y, edgesAreWalls);

                    // If the tile we are looking is at the edge
                    if (edgesAreWalls && (x == 0 || x == (map.GetUpperBound(0) - 1) || y == 0 || y == (map.GetUpperBound(1) - 1)))
                    {
                        //Set the edge to be a wall if we have edgesAreWalls to be true
                        map[x, y] = 1;
                    }

                    // Else if not the edge, if the number of surrounding tiles is greater than the threshold
                    else if (surroundingTiles > threshold)
                    {
                        // The cell becomes a wall
                        map[x, y] = 1;
                    }
                    // Else, if less than threshold
                    else if (surroundingTiles < threshold)
                    {
                        // The will not be a wall
                        map[x, y] = 0;
                    }
                }
            }
        }
        // Return the modified map
        return map;
    }
}

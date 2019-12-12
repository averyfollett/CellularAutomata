// This code file uses a script sourced from https://blogs.unity3d.com/2018/06/07/procedural-patterns-to-use-with-tilemaps-part-ii/
//  Comments and slight alterations have been made by Avery Follett, 12-05-2019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellularAutomata : MonoBehaviour
{
    // We will get a 2D int array of a size we specify
    //  width = width of generated map
    //  height = height of generated map
    //  seed = a seed which is used to generate the map (we should be able to generate the same map by using the same seed)
    //  fillPercent = the percentage (0-100) of the map which should be filled
    //  edgesAreWalls = true or false whether we want walls or not surrounding the map
    public static int[,] GenerateCellularAutomata(int width, int height, float seed, int fillPercent, bool edgesAreWalls)
    {
        // Seed our random number generator
        System.Random rand = new System.Random(seed.GetHashCode());

        // Initialise the map
        int[,] map = new int[width, height];

        // For each x location
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            // For each y location
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                // Using the current x,y coordinate...
                // If we have the edges set to be walls, ensure the cell is set to on (1)
                if (edgesAreWalls && (x == 0 || x == map.GetUpperBound(0) - 1 || y == 0 || y == map.GetUpperBound(1) - 1))
                {
                    // This cell is on the edge, therefore it has to be a wall
                    map[x, y] = 1;
                }
                else
                {
                    // This cell is not on the edge, so it has a change to be empty
                    // Randomly generate the grid
                    map[x, y] = (rand.Next(0, 100) < fillPercent) ? 1 : 0;
                }
            }
        }

        // Map has been generated
        return map;
    }
}

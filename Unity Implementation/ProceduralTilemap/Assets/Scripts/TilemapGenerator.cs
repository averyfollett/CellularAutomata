// This script was written by Avery Follett, 12-5-2019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapGenerator : MonoBehaviour
{
    // VARIABLES
    private int[,] map; // The 2D binary array containing the layout of the tilemap

    public GameObject wall;
    public int width, height, fillPercent, smoothCount, smoothingThreshold;
    public float seed;
    public bool edgesAreWalls;
    
    private void Update()
    {
        // When use presses space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Remove any existing walls
            GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
            foreach (GameObject wall in walls)
                GameObject.Destroy(wall);

            // CELLULAR AUTOMATA
            map = CellularAutomata.GenerateCellularAutomata(width, height, seed, fillPercent, edgesAreWalls);

            // MOORE NEIGHBORHOOD
            map = MooreNeighborhood.SmoothMooreCellularAutomata(map, edgesAreWalls, smoothCount, smoothingThreshold);

            // DRAW TILEMAP
            // For each x location
            for (int x = 0; x < map.GetUpperBound(0); x++)
            {
                // For each y location
                for (int y = 0; y < map.GetUpperBound(1); y++)
                {
                    // If that cell is not a wall, do nothing
                    if (map[x, y] == 0)
                    {
                        continue;
                    }
                    // Else, if that cell is a wall, create a wall gameobject
                    else if (map[x, y] == 1)
                    {
                        Instantiate(wall, new Vector3(x, y, 0f), Quaternion.identity);
                    }
                }
            }
        }
    }
}

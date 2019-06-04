using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public GameObject Block;

    // Start is called before the first frame update
    private void Start()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        // Path of level file
        string levelPath = "Assets/Levels/test.lvl";

        // Store the file's lies into a string table
        string[] Level = File.ReadAllLines(levelPath);
        // Read every lines
        foreach (string line in Level)
        {
            // Split string (Format: "<gameObject>,<x>,<y>,<z>")
            string[] data = line.Split(',');

            // Create a Block
            if (data[0].Contains("Block"))
            {
                GameObject.Instantiate(Block, new Vector3(float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3])), Quaternion.identity);
            }
            if (data[0].Contains("Option"))
            {
                if (data[1].Contains("GameLength"))
                {
                    // Do something.
                }

            }
        }
    }
}

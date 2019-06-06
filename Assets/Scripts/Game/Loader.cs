using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Loader : MonoBehaviour
{
    static public string level;
    public GameObject Player;
    public GameObject Block;
    public GameObject Item;

    // Start is called before the first frame update
    private void Start()
    {
        if (string.IsNullOrEmpty(level))
        {

            LoadLevel("test");
        }
        else
        {
            LoadLevel(StaticValues.level);
        }
    }

    /* Not used
    // Create an item with given position and rotation  
    private void CreateItem(float x, float y, float z, int rx, int ry, int rz)
    {
        float posX = x + (rx == 0 ? 0.25f : rx == 1 ? 0f : rx == 2 ? -0.25f : 0f);
        float posY = y + (ry == 0 ? 0.25f : ry == 1 ? 0f : ry == 2 ? -0.25f : 0f);
        float posZ = z + (rz == 0 ? 0.25f : rz == 1 ? 0f : rz == 2 ? -0.25f : 0f);
        Vector3 position = new Vector3(posX, posY, posZ);

        float rotX = rx == 0 ? 0f : rx == 1 ? 90f : rx == 2 ? 180f : 270f;
        float rotY = ry == 0 ? 0f : ry == 1 ? 90f : ry == 2 ? 180f : 270f;
        float rotZ = rz == 0 ? 0f : rz == 1 ? 90f : rz == 2 ? 180f : 270f;

        Quaternion rotation = Quaternion.Euler(rotX, rotY, rotZ);

        Instantiate(
            Item,
            position,
            rotation
        );
    }
    */

    private void LoadLevel(string lvl)
    {
        // Path of level file
        string levelPath = "Assets/Levels/" + lvl + ".lvl";

        // Store the file's content into a string table
        string[] Level = File.ReadAllLines(levelPath);

        // Read every lines
        foreach (string line in Level)
        {
            // Split string (Format: "<gameObject>,<x>,<y>,<z>")
            string[] data = line.Split(',');

            switch (data[0])
            {
                // Create the player
                case "Player":
                    Instantiate(
                        Player,
                        new Vector3(float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3])),
                        Quaternion.identity
                        );
                    break;
                // Create a Block
                case "Block":
                    Instantiate(
                        Block,
                        new Vector3(float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3])),
                        Quaternion.identity
                        );
                    break;
                // Create an Item
                case "Item":
                    Instantiate(
                        Item,
                        new Vector3(float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3])),
                        Quaternion.identity
                        );
                    break;
            }
        }
    }
}

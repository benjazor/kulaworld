using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject Player;
    public GameObject Block;
    public GameObject Item;
    public GameObject Coin;
    public GameObject BlueCoin;
    public GameObject RedGem;
    public GameObject GreenGem;
    public GameObject BlueGem;
    public GameObject KeyItem;
    public GameObject ExitItem;


    static public GameObject Exit;
    static public float duration;
    static public Vector3 spawn;
    static public int keys = 0;
    static public string next;

    // Start is called before the first frame update
    private void Awake()
    {
        if (string.IsNullOrEmpty(StaticValues.level))
        {
            LoadLevel("8");
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

    // Load the selected level file
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
                // Get the level duration (in seconds)
                case "Duration":
                    duration = float.Parse(data[1]);
                    break;
                // Get the next level
                case "Next":
                    next = data[1];
                    break;
                // Get the spawn point and move the player to it
                case "Player":
                    spawn = new Vector3(float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3]));
                    Player.transform.position = spawn;
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
                // Create a Coin
                case "Coin":
                    Instantiate(
                        Coin,
                        new Vector3(float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3])),
                        Quaternion.identity
                        );
                    break;
                // Create a Blue Coin
                case "BlueCoin":
                    Instantiate(
                        BlueCoin,
                        new Vector3(float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3])),
                        Quaternion.identity
                        );
                    break;
                // Create a Red Gem
                case "RedGem":
                    Instantiate(
                        RedGem,
                        new Vector3(float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3])),
                        Quaternion.identity
                        );
                    break;
                // Create a Green Gem
                case "GreenGem":
                    Instantiate(
                        GreenGem,
                        new Vector3(float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3])),
                        Quaternion.identity
                        );
                    break;
                // Create a Blue Gem
                case "BlueGem":
                    Instantiate(
                        BlueGem,
                        new Vector3(float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3])),
                        Quaternion.identity
                        );
                    break;
                // Create a Key
                case "Key":
                    keys += 1;
                    Instantiate(
                        KeyItem,
                        new Vector3(float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3])),
                        Quaternion.identity
                        );
                    break;
                // Create an Exit
                case "Exit":
                    Exit = Instantiate(
                        ExitItem,
                        new Vector3(float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3])),
                        Quaternion.identity
                        );
                    break;
            }
        }
    }
}

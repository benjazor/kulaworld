using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        // Item rotation
        transform.Rotate(Vector3.one * 20 * Time.deltaTime);
    }


    // When somthing colide the Item
    private void OnTriggerEnter(Collider other)
    {
        // Check if colider is a Player
        if (other.tag == "Player")
        {
            StaticValues.levelScore += 250;
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public Material on;
    public bool status;
    public bool win = false;

    private void Awake()
    {
        status = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Item rotation
        transform.Rotate(Vector3.one * 20 * Time.deltaTime);

        if (status)
        {
            GetComponent<Renderer>().material = on;
        }
    }


    // When somthing colide the Item
    private void OnTriggerEnter(Collider other)
    {
        // Check if colider is a Player
        if (other.tag == "Player" && status)
        {
            win = true;
        }
    }
}

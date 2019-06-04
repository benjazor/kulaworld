using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTrigger : MonoBehaviour
{
    // When somthing enters the block
    private void OnTriggerEnter(Collider other)
    {
        // Check if colider is a TriggerBlock
        if (other.tag == "TriggerBlock")
        {
            // Set TriggerBlock status to true
            other.GetComponent<Trigger>().status = true;
        }
    }

    // When somthing stays the block
    private void OnTriggerStay(Collider other)
    {
        // Check if colider is a TriggerBlock
        if (other.tag == "TriggerBlock")
        {
            // Set TriggerBlock status to true
            other.GetComponent<Trigger>().status = true;
        }
    }

    // When somthing exits the block
    private void OnTriggerExit(Collider other)
    {
        // Check if colider is a TriggerBlock
        if (other.tag == "TriggerBlock")
        {
            // Set TriggerBlock status to false
            other.GetComponent<Trigger>().status = false;
        }
    }
}

using UnityEngine;
using System.Threading.Tasks;
using System.Collections;
// Just for debug
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text debugText;

    // Triggers
    public GameObject TriggerObject;
    private GameObject TriggerA;
    private GameObject TriggerB;
    private GameObject TriggerC;
    private GameObject TriggerD;
    private GameObject TriggerE;

    // Movement varibables
    float lastMove = 0; // Time of the last movement input
    float movementDuration = 0.2f; // Movement duration in seconds

    private void Start()
    {
        // Create the triggers required for movement
        TriggerA = GameObject.Instantiate(TriggerObject, new Vector3(0, 0, 0), Quaternion.identity);
        TriggerB = GameObject.Instantiate(TriggerObject, new Vector3(0, 0, 0), Quaternion.identity);
        TriggerC = GameObject.Instantiate(TriggerObject, new Vector3(0, 0, 0), Quaternion.identity);
        TriggerD = GameObject.Instantiate(TriggerObject, new Vector3(0, 0, 0), Quaternion.identity);
        TriggerE = GameObject.Instantiate(TriggerObject, new Vector3(0, 0, 0), Quaternion.identity);
        UpdateTriggers();
    }

     // FixedUpdata is called once per frame
    void FixedUpdate()
    {
        MoveInput();

        // Debug
        string debugString = "";

        debugString += "DEBUG\n";
        debugString += "Position: " + transform.position + "\n";
        debugString += "Rotation: " + transform.rotation + "\n";

        debugText.text = debugString;
    }

    // Boolean value telling if the player is moving or not
    bool Moving() => (Time.time < (lastMove + 0.2));


    // Execute player movement if he is not moving.
    void MoveInput()
    {
        if (!Moving())
        {
            // Move Forwoard
            if (Input.GetKey("z"))
            {
                Move(1);
            }
            // Rotate Left
            else if (Input.GetKey("q") && !Input.GetKey("d"))
            {
                Move(2);
            }
            // Rotate Right
            else if (Input.GetKey("d") && !Input.GetKey("q"))
            {
                Move(3);
            }
            // Jump
            else if (Input.GetKey("space"))
            {
                Move(4);
            }
        }
        
    }

    void Move(int direction)
    {
        // Record the time of movement
        lastMove = Time.time;

        // Move in the right direction
        switch (direction)
        {
            // Move Forward
            case 1: 
                // Make a rotation when facing a wall
                if (TriggerA.GetComponent<Trigger>().status)
                {
                    transform.Rotate(new Vector3(-90, 0, 0));
                }
                // Simply move forward
                else if (TriggerB.GetComponent<Trigger>().status)
                {
                    transform.Translate(new Vector3(0, 0, 1));
                }
                // Move to another face of the block
                else if (!(TriggerD.GetComponent<Trigger>().status || TriggerE.GetComponent<Trigger>().status))
                {
                    transform.position += transform.forward - transform.up;
                    transform.Rotate(new Vector3(90, 0, 0));
                }
                break;
            // Rotate Left
            case 2:
                transform.Rotate(new Vector3(0, -90, 0));
                break;
            // Rotate Right
            case 3: 
                transform.Rotate(new Vector3(0, 90, 0));
                break;
            // Jump
            case 4:
                if (!TriggerA.GetComponent<Trigger>().status && !TriggerC.GetComponent<Trigger>().status)
                {
                    // Call the Jump coroutine
                    StartCoroutine(Jump(10));
                }
                break;
        }
        UpdateTriggers();
    }

    // Move the triggers with the player
    void UpdateTriggers()
    {
        TriggerA.transform.position = transform.position + transform.forward;
        TriggerB.transform.position = transform.position + transform.forward - transform.up;
        TriggerC.transform.position = transform.position + transform.forward * 2;
        TriggerE.transform.position = transform.position + transform.right - transform.up;
        TriggerD.transform.position = transform.position - transform.right - transform.up;
    }

    IEnumerator Jump(int i)
    {
        // DEBUG
        //print(TriggerC.GetComponent<Trigger>().status.ToString() + " " + TriggerC.transform.position.ToString());

        if (i == 0)
        {
            // Update the triggers
            UpdateTriggers();
            // Stop the coroutine
            yield return null;
        }
        // Check if we hit a block
        else if (TriggerC.GetComponent<Trigger>().status)
        {
            // Move the player on the block and end the loop
            transform.position = TriggerC.transform.position + transform.up;
            // Update the triggers
            UpdateTriggers();
            // Stop the coroutine
            yield return null;
        }
        else
        {
            // Trigger goes down
            TriggerC.transform.Translate(-transform.up);
            // Wait for physic update
            yield return new WaitForFixedUpdate();
            // Run the function again
            StartCoroutine(Jump(i - 1));
        }
    }
}
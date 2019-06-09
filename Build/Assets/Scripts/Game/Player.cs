using UnityEngine;
using System.Threading.Tasks;
using System.Collections;
// Just for debug
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Triggers
    public GameObject TriggerObject;
    private GameObject TriggerA;
    private GameObject TriggerB;
    private GameObject TriggerC;
    private GameObject TriggerD;
    private GameObject TriggerE;

    // Movement varibables
    private float lastMove = 0; // Time of the last movement input
    public float movementDuration; // Movement duration in seconds

    // Execute when a player is created
    private void Start()
    {
        // Create and update the triggers required for movement
        TriggerA = Instantiate(TriggerObject, new Vector3(0, 0, 0), Quaternion.identity);
        TriggerB = Instantiate(TriggerObject, new Vector3(0, 0, 0), Quaternion.identity);
        TriggerC = Instantiate(TriggerObject, new Vector3(0, 0, 0), Quaternion.identity);
        TriggerD = Instantiate(TriggerObject, new Vector3(0, 0, 0), Quaternion.identity);
        TriggerE = Instantiate(TriggerObject, new Vector3(0, 0, 0), Quaternion.identity);
        UpdateTriggers();
    }

    // FixedUpdate is called once per frame
    private void FixedUpdate()
    {

        // Check for input when the player isn't moving
        if (!Moving()) { MoveInput(); }

        // Debug
        //debugText.text = "DEBUG";
    }

    // Boolean value telling if the player is moving or not
    private bool Moving() => (Time.time < (lastMove + movementDuration));

    // Execute player movement if he is not moving.
    private void MoveInput()
    {
        if (Input.GetKey("z"))
        {
            Move(1); // Move Forwoard
        }
        else if (Input.GetKey("q") && !Input.GetKey("d"))
        {
            Move(2); // Rotate Left
        }
        else if (Input.GetKey("d") && !Input.GetKey("q"))
        {
            Move(3); // Rotate Right
        }
        else if (Input.GetKey("space"))
        {
            Move(4); // Jump
        }
    }

    private void Move(int direction)
    {
        // Record the time of movement
        lastMove = Time.time;

        // Move in the coorect direction
        //  0: Idle
        //  1: Forward
        //  2: Rotate Left
        //  3: Rotate Right
        //  4: Jump
        switch (direction)
        {
            case 1: // Move Forward
                // Make a rotation when facing a wall
                if (TriggerA.GetComponent<Trigger>().status)
                {
                    //transform.Rotate(new Vector3(-90, 0, 0));
                    StartCoroutine(SmoothRotation(new Vector3(-90, 0, 0)));
                }
                // Simply move forward
                else if (TriggerB.GetComponent<Trigger>().status)
                {
                    //transform.Translate(new Vector3(0, 0, 1));
                    StartCoroutine(SmoothMovement(new Vector3(0, 0, 1)));
                }
                // Move to another face of the block
                else if (!(TriggerD.GetComponent<Trigger>().status || TriggerE.GetComponent<Trigger>().status))
                {
                    //transform.Rotate(new Vector3(90, 0, 0));
                    Vector3 endPos = transform.position + transform.forward - transform.up;
                    StartCoroutine(SmoothMovement(new Vector3(0, 0, Mathf.PI * 2f / 4f)));
                    StartCoroutine(SmoothRotation(new Vector3(90, 0, 0)));
                }
                break;
            case 2: // Rotate Left
                //transform.Rotate(new Vector3(0, -90, 0));
                StartCoroutine(SmoothRotation(new Vector3(0, -90, 0)));
                break;
            case 3: // Rotate Right
                //transform.Rotate(new Vector3(0, 90, 0));
                StartCoroutine(SmoothRotation(new Vector3(0, 90, 0)));
                break;
            // Jump
            case 4:
                if (!TriggerA.GetComponent<Trigger>().status && !TriggerC.GetComponent<Trigger>().status)
                {
                    // Call the Jump coroutine
                    StartCoroutine(Jump(30));
                }
                break;
        }
        StartCoroutine(FixPosition());
        UpdateTriggers();
    }

    private IEnumerator SmoothMovement(Vector3 vector)
    {
        for (int i = 0; i < 10; i++)
        {
            transform.Translate(vector / 10f);
            UpdateTriggers();
            yield return new WaitForSeconds(movementDuration / 15);
        }
    }

    private IEnumerator FixPosition()
    {
        yield return new WaitForSeconds(movementDuration);
        Vector3 vec = transform.position;
        vec.x = Mathf.RoundToInt(vec.x);
        vec.y = Mathf.RoundToInt(vec.y);
        vec.z = Mathf.RoundToInt(vec.z);
        transform.position = vec;

    }

    private IEnumerator SmoothRotation(Vector3 vector)
    {
        for (int i = 0; i < 10; i++)
        {
            transform.Rotate(vector / 10f);
            UpdateTriggers();
            yield return new WaitForSeconds(movementDuration / 15);
        }
    }

    // Move the triggers with the player
    private void UpdateTriggers()
    {
        TriggerA.transform.position = transform.position + transform.forward;
        TriggerB.transform.position = transform.position + transform.forward - transform.up;
        TriggerC.transform.position = transform.position + transform.forward * 2;
        TriggerE.transform.position = transform.position + transform.right - transform.up;
        TriggerD.transform.position = transform.position - transform.right - transform.up;
    }

    private IEnumerator Jump(int i)
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
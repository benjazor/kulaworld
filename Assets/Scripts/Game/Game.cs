using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject Player;
    public GameObject UserInterface;

    private Text ElapsedTimeUI;
    private Text RemainingTimeUI;
    private Text TotalScore;
    private Text LevelScore;

    private float StartTime;
    private float EndTime;

    // Start is called before the first frame update
    void Awake()
    {
        // Set the start time to current time
        StartTime = Time.time;
        EndTime = Time.time + Loader.duration;

        // Check every children of UserInterface of "Text" type
        foreach (Text text in UserInterface.GetComponentsInChildren<Text>())
        {
            // bind every child to it's variable
            switch (text.name)
            {
                case "ElapsedTime": ElapsedTimeUI = text; break;
                case "RemainingTime": RemainingTimeUI = text; break;
                case "TotalScore": TotalScore = text; break;
                case "LevelScore": LevelScore = text; break;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if the player has beate the level
        checkWin();

        // Check if the player has all the keys
        checkKeys();

        // Gameover when RemainingTime is 0
        if (RemainingTime() <= 0f)
        {
            GameOver();
        }

        // Display remaining and elapsed time
        ElapsedTimeUI.text = "Remaining: " + Mathf.Round(RemainingTime()).ToString();
        RemainingTimeUI.text = "Elapsed: " + Mathf.Round(ElapsedTime()).ToString();
        TotalScore.text = "Total Score: " + StaticValues.totalScore.ToString(); 
        LevelScore.text = "Level Score: " + StaticValues.levelScore.ToString();

        if (Input.GetKey(KeyCode.K))
        {
            FlipTime();
        }
    }

    private float RemainingTime() => EndTime - Time.time;
    private float ElapsedTime() => Time.time - StartTime;

    // Flip the RemainingTime with the ElapsedTime (For the HourGlass)
    public void FlipTime()
    {
        float remaining = RemainingTime();
        float elapsed = ElapsedTime();
        // We store temporary values
        StartTime = Time.time - remaining;
        EndTime = Time.time + elapsed;
    }

    // Execute when the player dies
    public void GameOver()
    {
        //if (StaticValues.totalScore - StaticValues.levelScore <= 0)
        if (false)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            StartTime = Time.time;
            EndTime = Time.time + Loader.duration;
            Player.transform.position = Loader.spawn;
            Player.transform.rotation = Quaternion.identity;
        }
    }

    // Check the number of keys
    public void checkKeys()
    {
        if (StaticValues.keys == Loader.keys)
        {
            Loader.Exit.GetComponent<Exit>().status = true;
        }
    }

    public void checkWin()
    {
        if (Loader.Exit.GetComponent<Exit>().win)
        {
            if (Loader.next == "End")
            {
                StaticValues.totalScore += StaticValues.levelScore;
                StaticValues.levelScore = 0;
                SceneManager.LoadScene(3);
            }
            else
            {
                StaticValues.totalScore += StaticValues.levelScore;
                StaticValues.levelScore = 0;
                StaticValues.level = Loader.next;
                SceneManager.LoadScene(1);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    public Text wintext;

    // Start is called before the first frame update
    void Start()
    {
        wintext.text = StaticValues.totalScore.ToString();
    }
}

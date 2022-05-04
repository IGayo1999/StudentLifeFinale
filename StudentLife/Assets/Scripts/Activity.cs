using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*The class used for activities in the game*/
public class Activity : MonoBehaviour
{
    /*How much of each player attribute an activity gives. */
    public int fullness;
    public int energy;
    public int restfulness;

    /*How much time an activity takes to perform.*/
    public int hour;
    public int minute;


    /*How much motivation a task requires to perform or how much motivation a task gives after being performed. Motivation needed is positive, motivation given is negative.*/
    public int motivation;

    /*How many tasks have been done*/
    public int taskCompletion;

    /*Messages that appear in the canvases before a task has been done and after a task has been done that is customised for each activity*/
    public string taskDoneMessage;
    public string taskMessage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*Used to return an instance of activity to get the variables from it.*/
    public Activity getActivity()
    {
        return this;
    }
}

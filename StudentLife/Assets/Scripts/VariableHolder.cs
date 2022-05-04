using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableHolder : MonoBehaviour
{
    //Player attributes.
    public int fullness;
    public int energy;
    public int motivation;
    public int restfulness;
    public int peacefulness;

    //Calendar attributes.
    public int hour;
    public int minute;
    public int week;
    public int day;

    //Amount of tasks done vs amount of tasks to do.
    public int taskCompletion;
    public int taskMax;

    //Used to set the weekly screen.
    public bool weeklyScreen = false;

    //Used to delete previous instances for future playthroughs.
    public static VariableHolder instance;
    // Start is called before the first frame update

    //Initialise.
    void Start()
    {
        fullness = 50;
        energy = 50;
        restfulness = 50;
        motivation = 0;
        peacefulness = 0;
        day = 1;
        week = 1;
        hour = 8;
        minute = 30;
        taskCompletion = 0;
        taskMax = 3;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Used to set min and max of player attributes.
        if (fullness > 100)
        {
            fullness = 100;
        }
        if (fullness < 0)
        {
            fullness = 0;
        }
        if (energy > 100)
        {
            energy = 100;
        }
        if (energy < 0)
        {
            energy = 0;
        }
        if (restfulness > 100)
        {
            restfulness = 100;
        }
        if (restfulness < 0)
        {
            restfulness = 0;
        }

        //Used to set max of calendar attributes.
        if (hour >= 24)
        {
            day += 1;
            hour -= 24;
        }
        if (minute >= 60)
        {
            hour += 1;
            minute -= 60;
        }

        //Used to set weekly screen.
        if (day > 7)
        {
            
            week += 1;
                day = 1;
            peacefulness = (fullness + energy + restfulness) / 3;
        weeklyScreen = true;
            if (taskCompletion != taskMax)
            {
                peacefulness -= 50;
            }
            taskCompletion = 0;
            taskMax += 1;
        }

        if (taskCompletion > taskMax)
        {
            taskCompletion = taskMax;
        }




        //If moving rooms, do not destroy this instance unless game is reloaded.
    }
    private void Awake()
    {
        // Does another instance already exist?
        if (instance && instance != this)
        {
            // Destroy myself
            Destroy(gameObject);
            return;
        }

        // Otherwise store my reference and make me DontDestroyOnLoad
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

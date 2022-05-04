using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

//Script that controls the flow of the game.
public class GameController : MonoBehaviour
{
    //Player instance
    public Player player;

    //Scene that is currently running
    public int sceneNumber;

    //Scenes that the player can move to when trannsporting.
    public int destination1;
    public int destination2;

    //Text attributes inside the canvases.
    public TextMeshProUGUI fullnessUI;
    public TextMeshProUGUI energyUI;
    public TextMeshProUGUI restfulnessUI;
    public TextMeshProUGUI calendarUI;
    public TextMeshProUGUI tasksDone;
    public TextMeshProUGUI destination1text;
    public TextMeshProUGUI destination2text;

    //Canvases that ask for confirmation before performing a task or confirm that a task has been performed.
    public Canvas activityConfirmation;
    public Canvas transportConfirmation;
    public Canvas finalScreen;
    public Canvas didAct;

    //The weekly result screen.
    public Canvas weekly;

    //The variable holder that persists between rooms.
    public VariableHolder vh;

    // Start is called before the first frame update
    void Start()
    {
        //Get the variableholder.
        GameObject gameo = GameObject.FindGameObjectWithTag("VariableHolder");
        vh = gameo.GetComponent<VariableHolder>();

        //Get the player.
        GameObject player1= GameObject.FindGameObjectWithTag("Player");
        player = player1.GetComponent<Player>();

        //Make the canvases disappear.
        activityConfirmation.gameObject.SetActive(false);
        transportConfirmation.gameObject.SetActive(false);
        finalScreen.gameObject.SetActive(false);
        didAct.gameObject.SetActive(false);
        weekly.gameObject.SetActive(false);

        //Get the scene number.
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        //If scene is apartment.
        if (sceneNumber == 1)
        {
            destination1text.text = "1: The Park";
            destination2text.text = "2: University";
            destination1 = 2;
            destination2 = 3;

        }

        //If scene is park.
        else if (sceneNumber == 2)
        {
            destination1text.text = "1: Apartment";
            destination2text.text = "2: University";
            destination1 = 1;
            destination2 = 3;
        }

        //If scene is University.
        else if (sceneNumber == 3)
        {
            destination1text.text = "1: Apartment";
            destination2text.text = "2: The Park";
            destination1 = 1;
            destination2 = 2;
        }
        
        //Alter canvas to display work currently done and work to be done.
        tasksDone.text = vh.taskCompletion.ToString() + " / " + vh.taskMax.ToString();

        //Alter canvas that displays the visible player attributes.
        fullnessUI.text = "Fullness: " + vh.fullness.ToString();
        energyUI.text = "Energy: " + vh.energy.ToString();
        restfulnessUI.text = "Restfulness: " + vh.restfulness.ToString();

        //Adds an extra 0 before the hour variable for consistent layout if hour <10.
        if (vh.hour < 10)
        {
            calendarUI.text = "Week " + vh.week.ToString() + "\n" + "Day " + vh.day.ToString() + "\n" + "0" + vh.hour.ToString() + ":" + vh.minute.ToString();
        }
        else
        {
            calendarUI.text = "Week " + vh.week.ToString() + "\n" + "Day " + vh.day.ToString() + "\n" + vh.hour.ToString() + ":" + vh.minute.ToString();
        }

        //Adds an extra 0 before the minute variable for consistent layout if minute <10.
        if (vh.minute < 10)
        {
            calendarUI.text += "0";
        }


        //Used to start chain of actions that start the weekly screen and allow the player to exit it.
        if (vh.week > 1 && vh.day == 1)
        {
            if (weekly.gameObject.active != true && vh.weeklyScreen == true)
            {
                setWeeklyScreen();

            }

            //Used to allow the player to exit weekly screen.
            else if (didAct.gameObject.active != true)
            {
                if (Input.GetButtonDown("Confirm"))
                {
                    Time.timeScale = 1f;
                    weekly.gameObject.SetActive(false);
                    player.UIelements.gameObject.SetActive(true);
                    player.prompt.gameObject.SetActive(true);
                    vh.weeklyScreen = false;

                }

            }

            //Used to end the game if the player gets to the end of week 4.
            if (vh.week == 5 && weekly.gameObject.active != true)
            {
                endGame();
            }


        }

        //Used to get the player back to the title screen provided they finished the game.
        if (finalScreen.gameObject.active)
        {
                if (Input.GetButtonDown("Check"))
                {
                    SceneManager.LoadSceneAsync(0);
                }

        }


        //Used to transport the player to diferent scenes, which varies depending on the actual scene currently running.
        if (transportConfirmation.gameObject.active)
        {
            if (Input.GetButtonDown("destination1"))
            {
                SceneManager.LoadSceneAsync(destination1);

            }
            else if(Input.GetButtonDown("destination2")){
                SceneManager.LoadSceneAsync(destination2);
            }



        }




    }
    //Set the weekly screen canvas ready.
    void setWeeklyScreen()
    {
        Time.timeScale = 0f;

        player.UIelements.gameObject.SetActive(false);
            player.prompt.gameObject.SetActive(false);
        weekly.gameObject.SetActive(true);
    }
    //End the game.
    void endGame()
    {
        finalScreen.gameObject.SetActive(true);
    }

}

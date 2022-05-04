using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//A script used to define the behaviour of the controlled playable character.
public class Player : MonoBehaviour
{
    //Variables pertaining to the player movement.
    public float xAxis;
    public float yAxis;
    public float speed = 5f;
    public float rotationSpeed = 300f;
    public Vector3 dir;

    //A prompt that appears if the player gets near a potential activity, including transport.
    public GameObject prompt;

    //Assuming that the player doesn't get involved at an activity at the start.
    public int tag = -1;

    //Define the activity instance.
    public Activity activity;

    //Define the gamecontroller instance.
    public GameController gamec;

    //Define the canvases for confirmation screens.
    public Canvas confirmation;
    public Canvas didAct;
    public Canvas UIelements;
    public Canvas transport;
    public Canvas notCompleted;

    //Get the variable holder that persists throughout scenes.
    public VariableHolder vh;

    //Define the customisable messages for each activity.
    public TextMeshProUGUI activityMessage;
    public TextMeshProUGUI didActivityMessage;


    //Call upon the Game Controller instance to set the UI elements based on the interactions of the player with the environment (enemies and pickups).
    // public GameController gameController;

    [SerializeField]
    private Animator animator;
    // private Rigidbody rigidbody;



    //Used to initialise the variables of the player from the persistent variable holder.
    void Start()
    {
        GameObject gameo = GameObject.FindGameObjectWithTag("VariableHolder");
        vh = gameo.GetComponent<VariableHolder>();
        prompt.SetActive(false);
        notCompleted.gameObject.SetActive(false);

    }




    // Update is called once per frame
    void Update()
    {
        //Movement
        animator.SetBool("Walk", false);
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(xAxis, 0.0f, yAxis);
        movement.Normalize();
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
        //transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);

        //Walking animation if player is moving
        if (movement != Vector3.zero)
        {
            animator.SetBool("Walk", true);
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        //ALlow the player to confirm doing an activity if near one.
        if (prompt.active && tag == 0)
        {
            if (Input.GetButtonDown("Check"))
            {
                activity = confirmActivity(activity);
            }

        }

        //Allow the player to confirm transport if near one.
        if (prompt.active && tag == 1)
        {
            if (Input.GetButtonDown("Check"))
            {
                confirmDestination();
            }
        }

        //Confirm the activity to be performed.
        if (tag == 0 && activity != null)
        {
            if (didAct.gameObject.active)
            {
                if (Input.GetButtonDown("Confirm"))
                {
                    didActivity();
                }
            }

            //Activity has been confirmed and the results of that is to be shown.
            else if (prompt.active != true && confirmation.gameObject.active)
            {

                if (Input.GetButtonDown("Confirm"))
                {
                    if (activity.motivation <= vh.motivation)
                    {
                        activityResults(activity);
                    }
                    
                    //If tasks (such as doing coursework) needs motivation to perform and player does not have enough.
                    else
                    {
                        notDone();
                    }
                }

                //If player changes their mind.
                else if (Input.GetButtonDown("Deny"))
                {
                    Time.timeScale = 1f;
                    confirmation.gameObject.SetActive(false);
                    UIelements.gameObject.SetActive(true);
                    prompt.SetActive(true);
                }


            }


        }










    }

    void OnTriggerEnter(Collider collision)
    {
        //If near an activity.
        GameObject other = collision.gameObject;
        if (other.CompareTag("InteractionCollider"))
        {
            prompt.SetActive(true);
            activity = other.GetComponent<Activity>();
            activity.getActivity();
            tag = 0;

        }
        
        //If near a transport node.
        else if (other.CompareTag("FastTransport"))
        {
            prompt.SetActive(true);
            tag = 1;
        }

    }

    void OnTriggerExit(Collider collision)
    {
        //After exiting an activity/transport node.
        GameObject other = collision.gameObject;
        if (other.CompareTag("InteractionCollider") || other.CompareTag("FastTransport"))
            {
                prompt.SetActive(false);
            }

        }

    //Confirm the activity the player will perform.
        Activity confirmActivity(Activity activity)
        {
        Time.timeScale = 0f;
        activityMessage.text = activity.taskMessage;
            confirmation.gameObject.SetActive(true);
            UIelements.gameObject.SetActive(false);
            prompt.gameObject.SetActive(false);


        return activity;
 
 
        }
    
    //Load up the activity results canvas.
        void activityResults(Activity activity)
        {

            confirmation.gameObject.SetActive(false);
        didActivityMessage.text = activity.taskDoneMessage;
        didAct.gameObject.SetActive(true);

        }

    //Confirm the destinations to travel to.
        void confirmDestination()
        {
            transport.gameObject.SetActive(true);
            UIelements.gameObject.SetActive(false);
        prompt.gameObject.SetActive(false);

        }

    //Alter the player and calendar attributes after performing an activity.
    void didActivity()
    {
        Time.timeScale = 1f;
        vh.fullness += activity.fullness;
        vh.energy += activity.energy;
        vh.restfulness += activity.restfulness;
        vh.motivation -= activity.motivation;
        vh.taskCompletion += activity.taskCompletion;
        vh.hour += activity.hour;
        vh.minute += activity.minute;
        didAct.gameObject.SetActive(false);
        UIelements.gameObject.SetActive(true);
        prompt.SetActive(true);
    }

    //Stop a player from performing an activity if motivation is insufficient.
    void notDone()
    {
        StartCoroutine("notReady");
    }

    //Stop the game temporarily if motivation is not high enough to perform the task to tell user as such.
    IEnumerator notReady()
    {
  
        confirmation.gameObject.SetActive(false);
        didAct.gameObject.SetActive(false);
        notCompleted.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(3);

        Time.timeScale = 1f;

        notCompleted.gameObject.SetActive(false);
        UIelements.gameObject.SetActive(true);
        prompt.SetActive(true);



    }
  




}

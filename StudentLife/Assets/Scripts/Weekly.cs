using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weekly : MonoBehaviour
{
    //Message to be displayed in weekly screen.
    public string message;

    public int fullness; //representative of the hunger of a person.
    public int energy; //representative of the physical stamina/endurance of a person.
    public int motivation; //representative of how motivated a person is before doing a task.
    public int restfulness; //representative of how much sleep a person has gotten.
    public int peacefulness;
    
    //What week of play it is.
    public int week;
    
    //Manipulate the message and the weekly count in weekly result screen canvas.
    public TextMeshProUGUI messageUI;
    public TextMeshProUGUI weeklyCount;

    //Variable holder that persists in scenes.
    public VariableHolder vh;

    // Start is called before the first frame update

    //Grab persistent variable holder.
    void Start()
    {
        GameObject gameo = GameObject.FindGameObjectWithTag("VariableHolder");
        vh = gameo.GetComponent<VariableHolder>();

    }

    // Update is called once per frame
    void Update()
    {
        
        week = vh.week-1;

        //Set the weekly messages differently depending on player attribute
        if (vh.peacefulness <= 25)
        {
            message = "Your peacefulness seems to be low. If this is the case, you should have a meal, take a break and perform activities that have been proven to reduce stress very effectively such as exercise. However, if you still feel that is not enough, be sure to get help from a professional. REMEMBER: YOU ARE NOT ALONE!";
        }
        if (vh.motivation <= 25)
        {
            message = "Your motivation seems to be low. You should try and perform activities that help you to relax and make sure you are fully refreshed and nourished before tackling a task.";
        }
        else if(vh.fullness <= 25)
        {
            message = "Your fullness seems to be low. You should remember to eat as lack of nutrition can cause a myriad of problems not just physically but also mentally!";
        }
        else if(vh.energy <= 25)
        {
            message = "Your energy seems to be low. You should remember to perform physical activity every now and then as physical activity, especially strenous exercise has been shown to decrease stress levels compared to inactivity!";

        }
        else if(vh.restfulness <= 25)
        {
            message = "Your restfulness seems to be low. You should try and get as much sleep as possible as lack of sleep has been proven to increase the production of stress hormones, reduce mental clarity and reduce physical capabilities, which can hinder you in your day-to-day life!";
        }
        else if (peacefulness >= 50)
        {
            message = "Well done. You are doing well. Keep following the same routine you have followed so far as it has been effective in keeping you in a good state of health!";
        }

        //Set the message in the weekly results screen canvas.
        messageUI.text = message;
        
        //Set the correct week for the weekly result screen canvas.
        weeklyCount.text = "Week " + week + " Results: ";
        
        
    }
}

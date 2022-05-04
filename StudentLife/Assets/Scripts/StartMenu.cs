using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    void Start()
    {
        //Destroy any instances of variable holder for future playthroughs.
        if (VariableHolder.instance) Destroy(VariableHolder.instance.gameObject);
    }
    void Update()
    {
        //Load the start of the game.
        if (Input.GetButtonDown("Check"))
        {
            
            SceneManager.LoadSceneAsync(1);
        }
    }
}
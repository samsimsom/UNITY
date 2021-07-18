using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberWizard : MonoBehaviour
{
    // class variables
    int maxNumber = 1000;
    int minNumber = 1;
    int guess = 500;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("--- Welcome to Number Wizard ---");
        Debug.Log("Pick an number, don't tell me what it is...");
        Debug.Log($"The number you can pick between " +
                  $"{minNumber} to {maxNumber}");
        Debug.Log("Tell me if your nunmber is higher or " +
                  "lower than 500");
        Debug.Log("Push Up: Higher - Push Down: Lower - " +
                  "Push Enter: Correct");
    }

    // Update is called once per frame
    void Update()
    {
        // Press UpArrow
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Up Arrow Key was Pressed!");
            minNumber = guess;
            Debug.Log(guess);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Down Arrow Key was Pressed!");
            maxNumber = guess;
            Debug.Log(guess);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Return!");
        }
    }
}

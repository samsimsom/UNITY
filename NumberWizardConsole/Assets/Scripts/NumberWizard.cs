using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberWizard : MonoBehaviour
{
    // class variables
    private int minNumber;
    private int maxNumber;
    private int guess;

    private void StartGame()
    {
        minNumber = 1;
        maxNumber = 1000;
        guess = 500;
        
        Debug.Log("--- Welcome to Number Wizard ---");
        Debug.Log("Pick an number, don't tell me what it is...");
        Debug.Log($"The number you can pick between {minNumber} to {maxNumber}");
        Debug.Log($"Tell me if your nunmber is higher or lower than {guess}");
        Debug.Log("Push Up: Higher - Push Down: Lower - Push Enter: Correct");
        maxNumber += 1;
    }
    
    private void NextGuess()
    {
        guess = (maxNumber + minNumber) / 2;
        Debug.Log($"Is it higher or lower than {guess}");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        // Press UpArrow
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            minNumber = guess;
            NextGuess();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            maxNumber = guess;
            NextGuess();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("I am a genius!");
            StartGame();
        }
    }
}

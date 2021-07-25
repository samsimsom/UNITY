using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using Random = UnityEngine.Random;

public class TimeGame : MonoBehaviour
{
    // UI Variables
    public TextMeshProUGUI messageUI;
    public TextMeshProUGUI randomTimeUI;
    public TextMeshProUGUI startTextUI;
    public GameObject startUI;
    public GameObject gameUI;
    
    // Game Variables
    private int _waitTime;
    private float _roundStartTime;
    private bool _roundStarted;
   
    // Start is called before the first frame update
    void Start()
    {
        messageUI.text = "Press the space bar ones you " +
                         "think the allotted time is up.";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _roundStarted = false;
            
            float playerWaitTime = Time.time - _roundStartTime;
            float error = Mathf.Abs(_waitTime - playerWaitTime);

            string message = "";
            if (error < 0.15f)
            {
                message = "Outstanding!";
            } else if (error < 0.75f)
            {
                message = "Exceeds Expectations.";
            } else if (error < 1.25f)
            {
                message = "Acceptable.";
            } else if (error < 1.75f)
            {
                message = "Poor";
            } else
            {
                message = "Dredfull!";
            }

            messageUI.text = $"You waited for {playerWaitTime} seconds." +
                             $"That's {error} seconds off. {message}";
        }
    }

    private void SetNewRandomTime()
    {
        _waitTime = Random.Range(5, 21);
        _roundStartTime = Time.time;
        randomTimeUI.text = Convert.ToString(_waitTime);
    }
    
}

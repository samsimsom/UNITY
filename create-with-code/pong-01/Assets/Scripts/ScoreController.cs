using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScoreController : MonoBehaviour
{

    [SerializeField] private Text playerScoreText;
    [SerializeField] private Text enemyScoreText;
    [SerializeField] private int goalToWin;
    
    private int _playerScore = 0;
    private int _enemyScore = 0;


    public void GoalPlayer()
    {
        _playerScore++;
    }


    public void GoalEnemy()
    {
        _enemyScore++;
    }
    
    
    private void Update()
    {
        if (_playerScore >= goalToWin || _enemyScore >= goalToWin)
        {
            Debug.Log("Game Won!");
            SceneManager.LoadScene("GameOver");
        }
    }


    private void FixedUpdate()
    {
        playerScoreText.text = _playerScore.ToString();
        enemyScoreText.text = _enemyScore.ToString();
    }
}

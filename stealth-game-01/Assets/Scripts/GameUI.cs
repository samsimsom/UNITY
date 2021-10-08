using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{

    [SerializeField] private GameObject _gameLoseUI;
    [SerializeField] private GameObject _gameWinUI;
    
    private bool gameIsOver;
    
    
    void ShowGameWinUI()
    {
        OnGameOver(_gameWinUI);
    }


    void ShowGameLoseUI()
    {
        OnGameOver(_gameLoseUI);
    }


    private void OnGameOver(GameObject gameOverUI)
    {
        gameOverUI.SetActive(true);
        gameIsOver = true;
        Guard.OnGuardHasSpottedPlayer -= ShowGameLoseUI;
        FindObjectOfType<Player>().OnReachEndOfLevel -= ShowGameWinUI;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        Guard.OnGuardHasSpottedPlayer += ShowGameLoseUI;
        FindObjectOfType<Player>().OnReachEndOfLevel += ShowGameWinUI;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (gameIsOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

}

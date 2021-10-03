using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Text secondsSurvivedUI;
    private bool _isGameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        // subscription
        FindObjectOfType<PlayerController>().OnPlayerDeath += OnGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void OnGameOver()
    {
        gameOverScreen.SetActive(true);
        secondsSurvivedUI.text = Mathf.Round(Time.timeSinceLevelLoad).ToString();
        _isGameOver = true;
    }
}

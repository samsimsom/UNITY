using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject healthDynamic;
    private Player player;
    private Text healtDynamicText;


    // Start is called before the first frame update
    void Start()
    {
        // Health Dynamic Text Componentine ulasiyorum.
        healtDynamicText = healthDynamic.GetComponent<Text>();

        // Sahnede Player Objesini buluyor.
        GameObject playerObject = GameObject.Find("Player");
        // Player objesinin componentlerinden Player isimli olan
        // bu durumda Player Scripti oluyor. Onu aliyor ve Scripte
        // erismis oluyorum.
        player = playerObject.GetComponent<Player>();
    }


    public void GameOver()
    {
        print("Game Over!");
    }

    
    private void UpdateHealtBar()
    {
        healtDynamicText.text = player.playerHealth.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        UpdateHealtBar();
    }
}
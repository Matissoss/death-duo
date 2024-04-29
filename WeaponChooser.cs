using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WeaponChooser : MonoBehaviour
{
    private int player1Choose;
    private int player2Choose;
    private string textForp1;
    private string textForp2;
    [SerializeField] private Text player1;
    [SerializeField] private Text player2;

    private void Awake()
    {
        PlayerPrefs.SetInt("Player1Score", 0);
        PlayerPrefs.SetInt("Player2Score", 0);
    }
    private void Update()
    {
        player1.text = "Player 1 Weapon: " + textForp1;
        player2.text = "Player 2 Weapon: " + textForp2;
        if (Input.GetKeyDown(KeyCode.A))
        {
            player1Choose--;
        }
        else if(Input.GetKeyDown(KeyCode.D)) 
        {
            player1Choose++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            player2Choose--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            player2Choose++;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetInt("player1Gun", player1Choose);
            PlayerPrefs.SetInt("player2Gun", player2Choose);
            SceneManager.LoadScene("Game");
        }
        numberToStringConverter();
    }
    private void numberToStringConverter()
    {
        switch (player1Choose)
        {
            case 0:
                player1Choose = 5;
                break;
            case 1:
                textForp1 = "AK-47";
                break;
            case 2:
                textForp1 = "AWP";
                break;
            case 3:
                textForp1 = "Shotgun";
                break;
            case 4:
                textForp1 = "Glock-17";
                break;
            case 5:
                textForp1 = "Deagle";
                break;
            case 6:
                player1Choose = 1;
                break;
        }
        switch (player2Choose)
        {
            case 0:
                player2Choose = 5;
                break;
            case 1:
                textForp2 = "AK-47";
                break;
            case 2:
                textForp2 = "AWP";
                break;
            case 3:
                textForp2 = "Shotgun";
                break;
            case 4:
                textForp2 = "Glock-17";
                break;
            case 5:
                textForp2 = "Deagle";
                break;
            case 6:
                player2Choose = 1;
                break;
        }
    }
}

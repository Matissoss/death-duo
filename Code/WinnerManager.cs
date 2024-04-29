using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinnerManager : MonoBehaviour
{
    [SerializeField] private Text winner;
    [SerializeField] private GameObject background;
    private void Update()
    {
        if(PlayerPrefs.GetInt("Player1Status") == 1)
        {
            background.gameObject.SetActive(true);
            winner.gameObject.SetActive(true);
            winner.text = "Player 2 Wins!";
            WaitForResponse();
        }
        else if (PlayerPrefs.GetInt("Player2Status") == 1)
        {
            background.gameObject.SetActive(true);
            winner.gameObject.SetActive(true);
            winner.text = "Player 1 Wins!";
            WaitForResponse();
        }
        else
        {
            background.gameObject.SetActive(false);
            winner.gameObject.SetActive(false);
        }
    }
    private void WaitForResponse()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("End! :)");
            SceneManager.LoadScene("Game");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.SetInt("Player1Score", 0);
            PlayerPrefs.SetInt("Player2Score", 0);
            SceneManager.LoadScene("MainMenu");
        }
    }
}

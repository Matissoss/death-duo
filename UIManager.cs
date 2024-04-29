using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text score;
    [SerializeField] private Text player1Health;
    [SerializeField] private Text player1Ammo;
    [SerializeField] private Text player2Health;
    [SerializeField] private Text player2Ammo;
    private void Update()
    {
        score.text = PlayerPrefs.GetInt("Player1Score").ToString() + " : " + PlayerPrefs.GetInt("Player2Score").ToString();
        player1Health.text = "Health: " + PlayerPrefs.GetInt("Player1Health").ToString();
        player2Health.text = "Health: " + PlayerPrefs.GetInt("Player2Health").ToString();
        player1Ammo.text = PlayerPrefs.GetInt("Player1BulletsInMagazine").ToString() + "/" + PlayerPrefs.GetInt("Player1MagazineCapacity").ToString() + " " + PlayerPrefs.GetInt("Player1Bullets");
        player2Ammo.text = PlayerPrefs.GetInt("Player2BulletsInMagazine").ToString() + "/" + PlayerPrefs.GetInt("Player2MagazineCapacity").ToString() + " " + PlayerPrefs.GetInt("Player2Bullets");
    }
}

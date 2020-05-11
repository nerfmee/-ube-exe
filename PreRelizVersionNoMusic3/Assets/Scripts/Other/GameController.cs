using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject menu;
    public GameObject Playerr;
    public GameObject SpawnPosition;
    public Player player;
    [SerializeField] private Text HighScore;
    [SerializeField] private UnityAdsScript adsScript;
    [SerializeField] private Button _deflectButton;
    public bool MenuActive = true;

    public int numberOfGames;



    public void RespawnPlayer()
    {
        
        adsScript.ShowAds();
        Playerr.transform.position = SpawnPosition.transform.position;
        Playerr.SetActive(true);
        _deflectButton.onClick.Invoke();
        player.isAlivePlayer = true;
        MenuActive = true;
        menu.SetActive(false);
        player.healthh.SetActive(true);

    }
    
    public void Update()
    { 
        if (player.isAlivePlayer == false && MenuActive == true)
        {
            Save();
            menu.SetActive(true);
            MenuActive = false;
        }
    }

    public void RestartGame()
    {
        if (numberOfGames == 3)
        {
            adsScript.ShowAds();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    public void OnClickExit ()
    { 
        Application.Quit();
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("SCORE", player.coinsInt);
       
        PlayerPrefs.SetInt("GAMES", numberOfGames);
        if (!PlayerPrefs.HasKey("GAMESNOW"))
        {
            PlayerPrefs.SetInt("GAMESNOW",numberOfGames);
        }
        else
        {
            int games = PlayerPrefs.GetInt("GAMESNOW");
            if (games < numberOfGames)
            {
                PlayerPrefs.SetInt("GAMESNOW",numberOfGames);
            }
        }
       
     
        if (!PlayerPrefs.HasKey("HS"))
        {
            PlayerPrefs.SetFloat("HS",player.coinsInt);
        }
        else
        {
            float hs = PlayerPrefs.GetFloat("HS");
            HighScore.text ="HighScore: "+ PlayerPrefs.GetFloat("HS").ToString();
            if (hs < player.coinsInt)
            {
                PlayerPrefs.SetFloat("HS",player.coinsInt);
            }
        }
    }
}

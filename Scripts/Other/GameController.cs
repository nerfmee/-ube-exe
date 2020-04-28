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
   
    private int gamesNumber;

    public int numberOfGames;



    public void RespawnPlayer()
    {
        adsScript.ShowAds();
        Playerr.transform.position = SpawnPosition.transform.position;
        Playerr.SetActive(true);
        player.isAlivePlayer = true;
        menu.SetActive(false);
        player.healthh.SetActive(true);
        

    }
    
    
    
    
    public void Update()
    {
        
        
        if (player.isAlivePlayer == false )
        {
            menu.SetActive(true);

            Save();
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
      
       // numberOfGames = 0;
        Application.Quit();
    }
    //coinsText.text = coinsInt.ToString();
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
            Debug.Log(PlayerPrefs.GetInt("GAMES"));
            int games = PlayerPrefs.GetInt("GAMESNOW");

            gamesNumber = PlayerPrefs.GetInt("GAMESNOW");
             Debug.Log(PlayerPrefs.GetInt("GAMESNOW")+ "из else");    
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
          //  Debug.Log(PlayerPrefs.GetFloat("SCORE"));
            
           
            
            float hs = PlayerPrefs.GetFloat("HS");
            HighScore.text ="HighScore: "+ PlayerPrefs.GetFloat("HS").ToString();
           // Debug.Log(PlayerPrefs.GetFloat("HS")+ "из else");    
            if (hs < player.coinsInt)
            {
                PlayerPrefs.SetFloat("HS",player.coinsInt);
            }
        }
    }
}

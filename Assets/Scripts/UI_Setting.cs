using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class UI_Seting : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public void newgame()
    {
        SceneManager.LoadScene("Game"); // load scene: truyền vào tên màn
    }

    public void pause()
    {
        Time.timeScale = 0f; // Freeze game time
        GameIsPaused = true;
    }

    public void resume()
    {
        Time.timeScale = 1f; // Resume game time
        GameIsPaused = false;
    }

    public void exitGame()
    {
        Application.Quit(); // thoát game
    }


    public GameObject panelrank;
    public GameObject huongdan;
    public void hthuongdan()
    {
        huongdan.SetActive(true);
        setingpanel.SetActive(false);
    }
    public void anhuongdan()
    {
        huongdan.SetActive(false);
    }
    public void htrank()
    {
        panelrank.SetActive(true);

        pause();
    }

    public void anrank()
    {
        panelrank.SetActive(false);
        resume();
    }

    public GameObject setingpanel;

    
    public void htseting()
    {
        setingpanel.SetActive(true);
        pause();

    }

    public void anseting()
    {
        setingpanel.SetActive(false);
        resume();
    }


    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f; // Resume game time
        GameIsPaused = false;
    }
    public void scene0()
    {
        
        SceneManager.LoadScene(0);


    }
    public void LoadGame()
    {
        SceneManager.LoadScene(2);
       
    }
    public void BG()
    {
       
        SceneManager.LoadScene(1);
    }


    public void Restart()
    {
        Time.timeScale = 1f; // Ensure time scale is reset before reloading the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
        GameIsPaused = false;
    }

    public Slider _musiscSlider, _sfxSlider;
    
    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();
    } 
    public void ToggleSFX()
    {
        AudioManager.instance.ToggleSFX();
    }
    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(_musiscSlider.value);
    }
    public void SFXVolume()
    {
        AudioManager.instance.SFXVolume(_sfxSlider.value);
    }

    private int playerCoins = 0;
    public TextMeshProUGUI coinText; // Reference to the TextMeshProUGUI component

    public void AddCoins(int amount)
    {
        playerCoins += amount;
        Debug.Log("Coins: " + playerCoins);

        // Update the TextMeshProUGUI text to show the current coin count
        coinText.text = "Coins: " + playerCoins;
    }

}
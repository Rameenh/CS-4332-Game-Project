using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool LevelWin = false;
    bool LevelLoss = false;
    public Player player;
    public string SceneName;
    public AudioSource audioSource;

    public GameObject completeLevelUI;
    public GameObject loseLevelUI;

    public Beat beat;

    // Start is called before the first frame update
    void Start()
    {
        Scene CurrentScene = SceneManager.GetActiveScene();
        SceneName = CurrentScene.name;

        audioSource = beat.musicSource;
    }

    // Update is called once per frame
    void Update()
    { 
        Scene CurrentScene = SceneManager.GetActiveScene();
        SceneName = CurrentScene.name;
        // Make sure scraps are updated in Player.cs
        if (SceneName == "Level1")
        {
            if (player.scraps == 5)
            {
                LevelWin = true;
            }
        }
        else if (SceneName == "Level2")
        {
            if (player.scraps == 8)
            {
                LevelWin = true;
            }
        }
        else if (SceneName == "Level3")
        {
            if (player.scraps == 10)
            {
                LevelWin = true;
            }
        }
        else if (SceneName == "Level4")
        {
            if (player.scraps == 12)
            {
                LevelWin = true;
            }
        }
        else if (SceneName == "Level5")
        {
            if (player.scraps == 4)
            {
                LevelWin = true;
            }
        }

        if (beat.songPosition >= audioSource.clip.length)
        {
            LoseLevel();
            LevelLoss = true;
        }

        if (LevelWin == true)
        {
            CompleteLevel();
        }

        if (player.playerDead) LoseLevel();

    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        SceneName = data.level;
    }

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
        LevelWin = false; //sets it back to false for the next level
        player.Health = 5;
    }

    public void LoseLevel()
    {
        loseLevelUI.SetActive(true);
        LevelLoss = false;
        player.Health = 5;
    }
}

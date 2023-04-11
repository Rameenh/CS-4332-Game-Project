using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string level; //SceneName in GameManager.cs
    //player will start in the default position of the level in the beginning of the scene; meaning all healths will be full


    public PlayerData(GameManager gameManager)
    {
        level = gameManager.SceneName;
    }
}

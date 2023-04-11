using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Combatant
{
    public static Player Singleton;
    public PlanetMover Mover;
    public Weapon EquippedWeapon;
    public bool DoubleDamage;
    public int rateOfFire = 1;
    public int scraps = 0;
    public string sceneName = "";
    public int scrapsPerLevel = -1;
    public bool playerDead = false;
    public bool shotInBeat = false;
    public int songProgress = -1;

    public void Awake()
    {
        if (Singleton != null)
        {
            Debug.LogError("Player singleton is not null and another player has spawned!");
        }
        Singleton = this;
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        // Make sure scraps are updated in GameManager.cs
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Level1")
        {
            scrapsPerLevel = 5;
        }
        if (sceneName == "Level2")
        {
            scrapsPerLevel = 8;
        }
        if (sceneName == "Level3")
        {
            scrapsPerLevel = 10;
        }
        if (sceneName == "Level4")
        {
            scrapsPerLevel = 12;
        }
        if (sceneName == "Level5")
        {
            scrapsPerLevel = 4;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Beat.Singleton.InBeat(rateOfFire) && !shotInBeat)
            {
                EquippedWeapon.Use(this, Vector2.up);
                shotInBeat = true;
            }
        }

        if (!Beat.Singleton.InBeat())
        {
            shotInBeat = false;
        }

        songProgress = (int)(Beat.Singleton.songPosition / Beat.Singleton.musicSource.clip.length * 100);
    }

    private void OnGUI()
    {
        
        GUI.Label(new Rect(10, 0, 1000, 20), "Scraps: " + scraps + "/" + scrapsPerLevel);
        GUI.Label(new Rect(10, 10, 1000, 20), "Song Progress: " + songProgress + "%");
        GUI.Label(new Rect(10, 20, 1000, 20), "Invincibility: " + invincibility);
        GUI.Label(new Rect(10, 30, 1000, 20), "Double Damage: " + DoubleDamage);
        GUI.Label(new Rect(10, 40, 1000, 20), "Fire Rate Multiplier: " + rateOfFire);
    }

    public override void Die()
    {
        playerDead = true;
        base.Die();
    }

    public void IncreaseHealth()
    {
        if (Health < MaxHealth)  //only increase the health if the player has less than 5 health
        {
            Health++;
        }
    }

    public void setInvincibility(bool value)
    {
        invincibility = value;
    }

    public void tempIncreaseDamage()
    {
        StartCoroutine(DoubleDamageWearOff(30));
    }
    IEnumerator DoubleDamageWearOff(float waitTime)
    {
        EquippedWeapon.Damage = 2;
        DoubleDamage = true;
        yield return new WaitForSeconds(waitTime);
        EquippedWeapon.Damage = 1;
        DoubleDamage = false;
    }

    public void tempDoubleRateOfFire()
    {
        StartCoroutine(DoubleROFWearOff(30));
    }
    IEnumerator DoubleROFWearOff(float waitTime)
    {
        //create a mechanism that can alter the rate of fire 
        rateOfFire = 2;
        yield return new WaitForSeconds(waitTime);
        //set the mechanism that resets the rapid fire mechanism
        rateOfFire = 1;
    }
}

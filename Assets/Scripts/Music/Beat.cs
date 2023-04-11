using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class Beat : MonoBehaviour
{
    public static Beat Singleton;
    //triggered on the first frame of the beat, argrument is beat number
    public static event System.Action<int> OnBeatEnter;
    public static event System.Action<int> OnBeatExit;

    //parallel arrays
    [Header("Make sure you have the same number of sections!")]
    public float[] SectionBpm;
    [Header("First value should be 0")]
    public float[] SectionStartSeconds;

    //Current song position, in seconds
    [field: SerializeField]
    public float songPosition { get; private set; }

    //How many seconds have passed since the song started
    public float dspSongTime { get; private set; }

    //in beats. Calculated using bpm of the current section
    [field: SerializeField]
    public float sectionBeatPosition { get; private set; }

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    //how close you have to be to the beat for it to count
    public float beatThreshold = 0.1f;

    //specifies when song starts
    public float songStart = 0;

    //number of beats that have elapsed
    private int beatCount = 0;

    private bool lastFrameInBeat = false;

    public string text; //for hit/miss indicator

    public float BeatScore(float multiplier = 1, int debugOutput = 0)
    {
        //how far after the last beat we are (from 0 to 1)
        float offset = multiplier * sectionBeatPosition - Mathf.Floor(multiplier * sectionBeatPosition);

         if (debugOutput != 0)
        {
            Debug.Log("Offset: " + offset);
        }

        //consider both cases: that we are early or late, and take the minimum offset
        return Mathf.Min(offset, 1 - offset);
    }

    public bool InBeat(float multiplier = 1) { return BeatScore(multiplier) < beatThreshold; }

    //gets bpm songTime seconds into the song
    public float GetBPM(float songTime)
    {
        return GetBPM(GetSection(songTime));
    }
    //gets bpm songTime seconds into the song
    public float GetBPM(int section)
    {
        if (section < 0)
        {
            //If all sections start after songTime, return 0 bpm
            return 0;
        }
        return SectionBpm[section];
    }
    public int GetSection(float songTime)
    {
        //returns index of the last section that starts before songTime 
        for (int i = SectionStartSeconds.Length - 1; i >= 0; i--)
        {
            if (SectionStartSeconds[i] <= songTime)
            {
                return i;
            }
        }
        //returns -1 if not in section
        Debug.LogWarning($"No song sections starting before time {songTime}!");
        return -1;
    }

    void Awake()
    {
        if (Singleton != null)
        {
            Debug.LogError("Beat already in the scene! Replacing it...");
            Destroy(Singleton.gameObject);
        }
        Singleton = this;
        //validate song sections
        if (SectionBpm.Length != SectionStartSeconds.Length)
        {
            Debug.LogError("SectionBpm and SectionStartSeconds arrays have different lengths!");
        }
        if (SectionStartSeconds.Length == 0)
        {
            Debug.LogWarning("No song sections!");

        }
        else
        {
            if (SectionStartSeconds[0] != 0)
            {
                Debug.LogWarning("The first song section doesn't start at time 0!");
            }
        }

        for (int i = 1; i < SectionStartSeconds.Length; i++)
        {
            if (SectionStartSeconds[i - 1] >= SectionStartSeconds[i])
            {
                Debug.LogWarning($"There is a song section at index {i} that starts before the previous section. It will not be used.");
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        // Change time that music starts
        musicSource.time = songStart;

        //Start the music
        musicSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused)
        {
            musicSource.Pause();
        }

        if (!(PauseMenu.GameIsPaused))
        {
            musicSource.UnPause();
        }
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime) - 0.2f + songStart;
        if (songPosition < 0) return; //there's some time before the song starts normally.

        int section = GetSection(songPosition);

        if (section < 0)
        {
            Debug.LogWarning("Currently not in a song section!");
            return;
        }
        float bpm = GetBPM(section);
        float sectionPosition = songPosition - SectionStartSeconds[section];

        float secondsPerBeat = 60.0f / bpm;
        sectionBeatPosition = sectionPosition / secondsPerBeat;


        if (!lastFrameInBeat && InBeat())
        {
            lastFrameInBeat = true;
            if (OnBeatEnter != null)
                OnBeatEnter(beatCount);
            beatCount++;
        }
        else if (lastFrameInBeat && !InBeat())
        {
            lastFrameInBeat = false;
            if (OnBeatExit != null)
                OnBeatExit(beatCount);
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            showBeatHitMiss();
        }
    }

    public void showBeatHitMiss()
    {
        if(InBeat())
        {
            text = "In Beat";
            StartCoroutine(labelDissapearTimer(1, text));
        }
        else
        {
            text = "Miss";
            StartCoroutine(labelDissapearTimer(1, text));
        }
    }
    IEnumerator labelDissapearTimer(float waitTime, string text)
    {
        yield return new WaitForSeconds(waitTime);
        text = "";
    }

    private void OnGUI()
    {
        
        //var centeredStyle = GUI.skin.GetStyle("Label");
        var centeredStyle = new GUIStyle(GUI.skin.label);
        centeredStyle.fontSize = 20;
        // centeredStyle.alignment = TextAnchor.LowerCenter;
        if (text == "Miss")
        {
            centeredStyle.normal.textColor = Color.red;
        }
        else
        {
            centeredStyle.normal.textColor = Color.green;
        }
        GUI.Label (new Rect (Screen.width/2 -100, Screen.height/2, 200, 200), text, centeredStyle);
    }
}

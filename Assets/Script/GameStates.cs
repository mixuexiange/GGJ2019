﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStates : MonoBehaviour {

    public static GameStates instance = null;

    public bool isSaving = true;

    // level data
    public static int curLevelID = 0;
    public static int unlockedLevelID = 0;

    // audio
    public static bool isAudio = true;

    [Range(0, 100f)] 
    public static float bgmVolume = 1.0f;

    [Range(0, 100f)]
    public static float sfxVolume = 1.0f;


    private void Awake()
    {
        //Check if there is already an instance 
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance
            Destroy(gameObject);

        //Set AudioManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);

        
    }


    // Use this for initialization
    void Start()
    {
        if (isSaving)
        {
            LoadLevel();
            LoadSettings();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    // save level after quit
    private void OnApplicationQuit()
    {
        if (isSaving)
            SaveLevel();
        else
            // clear keys
            PlayerPrefs.DeleteAll();
    }


    // save system settings data and current level
    public static void SaveLevel()
    {
        PlayerPrefs.SetInt("hasSavedLevel", 1);

        curLevelID = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("curLevelID", curLevelID);

        if (unlockedLevelID < curLevelID)
            unlockedLevelID = curLevelID;

        PlayerPrefs.SetInt("unlockedLevelID", unlockedLevelID);

        PlayerPrefs.Save();
        print("save level: " + curLevelID);

    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("hasSavedSettings", 1);

        PlayerPrefs.SetFloat("bgmVolume", bgmVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.Save();

        print("save settings");

    }

    public void LoadLevel()
    {
        print("LoadLevel");

        // check if it is the first time playing
        if (PlayerPrefs.HasKey("hasSavedLevel"))
        {

            curLevelID = PlayerPrefs.GetInt("curLevelID");

            unlockedLevelID = PlayerPrefs.GetInt("unlockedLevelID");

            print("load level: " + curLevelID);


            // go to curLevel
            SceneManager.LoadScene(curLevelID);
        }


    }

    public static void LoadSettings()
    {
        // check if it is the first time playing
        if (PlayerPrefs.HasKey("hasSavedSettings"))
        {
            bgmVolume = PlayerPrefs.GetFloat("bgmVolume");
            sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
 

            // to do: apply settings 
        }
    }

}

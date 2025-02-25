﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneHandler : MonoBehaviour
{
    public static string MAIN_MENU_SCENE= "Main Menu";
    public static string LEVEL_LIST_SCENE = "Level List";
    public static string LEVEL_ENTRY_SCENE = "Level Entry";
    public static string LEVEL_STORE_SCENE = "Level Store";
    public static string ALADDIN_LAMP_SCENE = "Aladdin Lamp";
    public static string LEVEL_PLAY_SCENE = "Level Play";
    public static string LEVEL_RESULT_SCENE = "Level Result";
    public static string SETTING_SCENE = "Setting";
    public static string INSTRUCTION = "Instruction";
    public static string PREVIOUS_SCENE_KEY = "previous_scene";
    private static SceneHandler instance;
    private static Stack<string> scene_history = new Stack<string>();
    private static string previous_scene;

    public static SceneHandler Instance {
        get {
            if (instance == null) {
                instance = new GameObject("SceneHandler").AddComponent<SceneHandler>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }
    private void Awake() {
        scene_history.Push(MAIN_MENU_SCENE);
    }

    public void OpenScene(string toScene) {
        scene_history.Push(toScene);
        string pre_scene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString(PREVIOUS_SCENE_KEY, pre_scene);

        LoadScene(toScene);

    }
    
    public void LoadScene(string scene) {
        SceneManager.LoadScene(scene);
        Debug.Log("Load Scene");
    }

    public bool GoBack() {
        bool return_value = false;
        Debug.Log("Pre Scene  :" + scene_history.Peek());
        if (scene_history.Count >= 2  )  //Checking that we have actually switched scenes enough to go back to a previous scene
        {
            return_value = true;
            scene_history.Pop();
            LoadScene(scene_history.Peek());
        }

        return return_value;

    }

    public void Exit() {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool tutorialLoaded;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        // Load Prototype105 scene
        if(!tutorialLoaded)
        {
        SceneManager.LoadScene("Game 1");
        tutorialLoaded = true;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    private int pressCount = 0;
    public TextMeshProUGUI screenText;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            pressCount ++;
            //Debug.Log(pressCount);
        }
        if(pressCount == 0)
        {
            transform.position = new Vector3(0f, 0f, 0f);
            screenText.text = "This is you. You are the DJ on your way to a party. Which one? You don´t know yet."; 
        }
        if(pressCount == 1)
        {
            transform.position = new Vector3(0f, 0f, 10f);
            screenText.text = "Unfortunately, all your music has been scattered around the place and you have to collect it from the different rooms.";
        }
        if(pressCount == 2)
        {
            transform.position = new Vector3(0f, 0f, -10f);
            screenText.text = "Listen closely which track you need for your song, then press Space infront of the door to enter."; 
        }
        if(pressCount == 3)
        {
            transform.position = new Vector3(10f, 0f, 0f);
            screenText.text = "When you´re done collecting your music, either go to the Techno-Party..."; 
        }
        if(pressCount == 4)
        {
            transform.position = new Vector3(-10f, 0f, 0f);
            screenText.text = "...or the Black-Metal Show to end the Game. But be careful not to bring the wrong music."; 
        }
        if(pressCount >=5)
        {
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene("Game");
    }
}

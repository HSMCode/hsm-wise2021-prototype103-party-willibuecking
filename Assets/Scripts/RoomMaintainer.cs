using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//This script is used for maintaining lists of room-prefabs, audiotracks and setting canvasses for scoredisplay at the end
//it also takes care of score-calculation and ends the game

public class RoomMaintainer : MonoBehaviour
{
    public List<GameObject> partyRooms;     //a list of the two partyroom-prefabs
    public GameObject musicRoomA;       //the black-metal-musicroom prefab
    public GameObject musicRoomT;       //the techno-musicroom prefab
    public bool aPartyBuildFlag;        //a flag used by "RoomController.cs" for alternating between building techno- and blackmetal-parties
                                        //it is stored here, because this script is attached to as gameObject that doesn´t get destroyed
    public List<GameObject> ambienceTracks; //a list for the BlackMetal Tracks
    public List<GameObject> technoTracks;   //a list for the Techno Tracks

    public bool aListEmpty;     //two flags for noting if all tracks have been used up of a certain genre. This is used by RoomMusicController.cs as a condition
    public bool tListEmpty;     //the fact that these are two seperate flags for both genres stems from previous versions where tracks could be called multiple times until they were collected
                                //in this version of the game, each track will only be called once but I am too afraid to change it now as it is deeply nested in the game and i don´t want to acciendtially break anything
    public bool finished = false;   //a flag to see if the game has been finished. This is set true by the PartyRooms once the player enters one of them

    public int score;       //a variable for the score. The PartyRooms write to this in the end when the player finishes the game. The value is then displayed by this script

    public GameObject outroPanel;       //the UI-Panel used at the end of the game to display the score and a semi-funny sentence of text using memes and idioms found in the context of the music-industry and indie-music-production scenes

    //private bool gameStarted = false;

    public TextMeshProUGUI scoreText;       //The text containing players score, displayed at the end of the game
    public TextMeshProUGUI gameOverMessage; //a semi-funny game over message mocking or praising the player, depending on the score reached

    private string gameLost;        //three strings containing messages for the end of the game (see above for more info on the content)
    private string gameWon;         //which one is displayed depends on the score
    private string gameMedium;

    public int tracksLeft = 6;      //variable to store the number of tracks left on the tracklists. This is used by RoomMusicController.cs each time a MusicRoom is built

    // Start is called before the first frame update
    void Start()
    {

        gameLost = "You came to the wrong party! Good Luck rebuilding your career.";        //set the three different texts for the end-of-game display
        gameWon = "Nice Set!\n I´ll recommend you to my friends, you´ll be drowning in exposure by next week.";
        gameMedium = "Meh, I´ve heard better but it´s not complete rubbish I guess.";

        outroPanel.SetActive(false);        //make sure the outro-panel is definitely not visible before the end of the game

        ambienceTracks.Add(GameObject.Find("A1"));      //find and add the 10 AudioTracks placed in the scene, each to their corresponding List
        ambienceTracks.Add(GameObject.Find("A2"));
        ambienceTracks.Add(GameObject.Find("A3"));
        ambienceTracks.Add(GameObject.Find("A4"));
        ambienceTracks.Add(GameObject.Find("A5"));

        technoTracks.Add(GameObject.Find("T1"));
        technoTracks.Add(GameObject.Find("T2"));
        technoTracks.Add(GameObject.Find("T3"));
        technoTracks.Add(GameObject.Find("T4"));
        technoTracks.Add(GameObject.Find("T5"));
    }

    // Update is called once per frame
    void Update()
    {

        if(ambienceTracks.Count == 0)       //this is from an older version when tracks where removed from the list when built. It is still used as a condition in RoomMusicController.cs so it is left in here too
        {
            aListEmpty = true;
        }

        if(technoTracks.Count == 0)     //these were both used to avoid NREs if all tracks were used and the player didn´t enter a PartyRoom right away
        {
            tListEmpty = true;
        }

        if(finished)
        {
            StartCoroutine("GameOverTimer");       //if the player enters a Partyroom and thus the finished-flag is set to true, start a coroutine that waits for 2 seconds and the calls the GameOver-function
                                                //the wait-time is used so the score can be calculated before it is displayed
        }
    }

    void GameOver()     //this is called once the game is finished and the GameOverTimer coroutine has finished waiting. The score and gameOverMessage are then displayed on screen
    {
        outroPanel.SetActive(true);
        scoreText.text = "You scored " + score.ToString() + " points";      //write the actual score into the score-display
        if(score >= 25)     //depending on the score, write a sentence accompanying the score-display
        {                   //these sentences are encouraging if the score is high (not less than 5 below maximum), mocking if the score is medium and insulting if the score is very low
            gameOverMessage.text = gameWon;
        }
        else if(score >= 5)
        {
            gameOverMessage.text = gameMedium;
        }
        else if (score < 5)
        {
            gameOverMessage.text = gameLost;
        }
    }

    IEnumerator GameOverTimer()     //this waits 2 seconds after the palyer enters the finishing PartyRoom before displaying the ending-texts onscreen
    {
        yield return new WaitForSeconds(2);
        GameOver();
    }
}
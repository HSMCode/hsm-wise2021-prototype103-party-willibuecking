using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomMaintainer : MonoBehaviour
{
    public List<GameObject> partyRooms;
    public GameObject musicRoomA;
    public GameObject musicRoomT;
    public GameObject emptyRoom;
    public bool aPartyBuildFlag;

    public List<GameObject> ambienceTracks;
    public List<GameObject> technoTracks;

    public bool aListEmpty;
    public bool tListEmpty;

    public bool finished = false;

    public int score;

    public GameObject introPanel;
    public GameObject outroPanel;

    private bool gameStarted = false;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverMessage;
    //public TextMeshProUGUI timeText;
    //public TextMeshProUGUI roomsVisited;

    public int roomCount;

    private string gameLost;
    private string gameWon;
    private string gameMedium;

    private float timer;

    public int tracksLeft = 6;

    // Start is called before the first frame update
    void Start()
    {

        gameLost = "You came to the wrong party! Good Luck rebuilding your career.";
        gameWon = "Nice Set!\n I´ll recommend you to my friends, you´ll be drowning in exposure by next week.";
        gameMedium = "Meh, I´ve heard better but it´s not complete rubbish I guess.";

        introPanel.SetActive(false);
        outroPanel.SetActive(false);

        ambienceTracks.Add(GameObject.Find("A1"));
        ambienceTracks.Add(GameObject.Find("A2"));
        ambienceTracks.Add(GameObject.Find("A3"));
        ambienceTracks.Add(GameObject.Find("A4"));
        ambienceTracks.Add(GameObject.Find("A5"));

        technoTracks.Add(GameObject.Find("T1"));
        technoTracks.Add(GameObject.Find("T2"));
        technoTracks.Add(GameObject.Find("T3"));
        technoTracks.Add(GameObject.Find("T4"));
        technoTracks.Add(GameObject.Find("T5"));

        //Debug.Log(tracksLeft);
    }

    // Update is called once per frame
    void Update()
    {

        if(ambienceTracks.Count == 0)
        {
            aListEmpty = true;
        }

        if(technoTracks.Count == 0)
        {
            tListEmpty = true;
        }

        if(finished)
        {
            StartCoroutine("GameOverTimer");
            
        }
        if(!finished && gameStarted)
        {
        timer += Time.deltaTime;
        }
    }

    void GameOver()
    {
        outroPanel.SetActive(true);
        scoreText.text = "You scored " + score.ToString() + "points";
        //timeText.text = "It took you " + timer.ToString() + " seconds.";
        //roomsVisited.text = "You visited " + roomCount.ToString() + " rooms.";
        if(score >= 25)
        {
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

    IEnumerator GameOverTimer()
    {
        yield return new WaitForSeconds(2);
        GameOver();
    }
}
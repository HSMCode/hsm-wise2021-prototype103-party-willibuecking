using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the AudioTracks and is attached to all of them

public class MusicMover : MonoBehaviour
{
    private bool isCollected = false;
    private GameObject player;
    private Vector3 playerPos;

    private GameObject sceneMaster;
    private RoomMaintainer roomMaintainerScript;

    private bool firstEnter = true;

    AudioSource thisSource;
    private float initVol;

    public int pointsA;     //variables for attaching score-points to the audiotracks. Each track has its own set of points, so this is public to change it individually and use the same script for all tracks
    public int pointsT;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerBody");
        playerPos = GameObject.Find("PlayerBody").transform.position;       //find the PlayerBody and get its transform.position for later on
        thisSource = GetComponent<AudioSource>();
        initVol = thisSource.volume;        //assign the values of the audiosource to initVol (each Track has its own volume)

    }

    // Update is called once per frame
    void Update()
    {
        if(isCollected)     //if the player collects the track, its volume is reduced to make uncollected tracks more audible
        {
            thisSource.volume = initVol - 0.5f;
        }
    }

    void OnTriggerEnter(Collider other)     
    {
        if(other.tag == "Player" && firstEnter)     //if the player enters the trigger-collider of this audiotrack for the first time
        {
            isCollected = true;     //the track is set to be collected
            transform.parent = player.transform;        //the transform.parent of this track is set to be the playerBody so it moves with the player
            firstEnter = false;     //the firstenter-flag is set to false so this doesn´t loop
        }
        if(other.tag == "FinishRoom")       //if the player brings this track into a finish-room (a partyRoom):
        {
            transform.parent = other.gameObject.transform;  //the transform.parent of this track is now the partyroom
            transform.position = other.gameObject.transform.position;       // and the track is moved to the middle of the room for more comfortable listening
        }
    }

    void OnTriggerExit(Collider other)   
    {
        if(other.tag == "MusicSwitcher" && !isCollected)        //if a room exits this tracks trigger-collider (because it moves rapidly away from the camera):
        {
            transform.position = new Vector3(0f, 100f, 0f);         //this track is moved out of audible distance from the player
        }
    }
}

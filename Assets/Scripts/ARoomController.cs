using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls specifically the Black-Metal PartyRoom

public class ARoomController : MonoBehaviour
{
    private GameObject sceneMaster;
    private RoomMaintainer roomMaintainerScript;

    public Light strobe;
    public Light redLight;
    private bool strobeActive;
    private int strobeCounter;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("StrobeInit", 0f, 0.25f);   //enables the strobe effect by calling the strobe-function (see below)
        strobe.enabled = false;

        sceneMaster = GameObject.Find("SceneMaster");
        roomMaintainerScript = sceneMaster.GetComponent<RoomMaintainer>();  //gets the RoomMaintainerScript from the SceneMaster for scoring and finishing purposes
    }

    // Update is called once per frame
    void Update()
    {
        redLight.intensity = Mathf.PingPong(Time.time*3f, 1.5f);    //slowly fades the red light in and out
    }

    void StrobeInit()       //switches the strobe-light from its previous state to the other one when called
    {
        strobe.enabled = !strobe.enabled;
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.tag == "AudioTrack")       //adds and subtracts the points of the single tracks collected by the player from the total score
        {
            roomMaintainerScript.score += other.gameObject.GetComponent<MusicMover>().pointsA;  
            roomMaintainerScript.score -= other.gameObject.GetComponent<MusicMover>().pointsT;
        }
        if(other.tag == "Player")       // tells the SceneMaster that the game is finished (which causes it to display the finishing-text)
        {
            roomMaintainerScript.finished = true;
        }
    }
}

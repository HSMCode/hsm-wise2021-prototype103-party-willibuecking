using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script specifically controls only the Techno-PartyRoom

public class TRoomController : MonoBehaviour
{
    private GameObject sceneMaster;
    private RoomMaintainer roomMaintainerScript;


    public GameObject lightRotator;
    public GameObject strobe;
    private bool strobeState = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StrobeEffect");     //initiates the Strobe-effect on startup (this one is much slower than the one of the Black-Metal PartyRoom)

        sceneMaster = GameObject.Find("SceneMaster");
        roomMaintainerScript = sceneMaster.GetComponent<RoomMaintainer>();  //gets the RoomMaintainerScript from the SceneMaster for scoring and finishing purposes
    }

    // Update is called once per frame
    void Update()
    {
      lightRotator.transform.Rotate(0.0f, 90.0f*Time.deltaTime, 0.0f, Space.Self);      //rotates a colourful arrangement of partylights continuously additional to the strobe-effect
      
      strobe.SetActive(strobeState);       //writes the strobe-light state (on or off) written to a boolean variable in the strobe-coroutine, to the actual strobe-light object
    }

    IEnumerator StrobeEffect()      //this coroutine takes care of the strobe effect, which is comparatively slow
    {
        yield return new WaitForSeconds(0.25f);     //the coroutine waits a quarter second
        strobeState = !strobeState;         //then changes lightstate-variable (from on to off or off to on)
        StartCoroutine("StrobeEffect");     //then calls itself again so the effect goes on forever
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.tag == "AudioTrack")       //adds and subtracts the points of the single tracks collected by the player from the total score
        {
            roomMaintainerScript.score -= other.gameObject.GetComponent<MusicMover>().pointsA;
            roomMaintainerScript.score += other.gameObject.GetComponent<MusicMover>().pointsT;
        }
        if(other.tag == "Player")        // tells the SceneMaster that the game is finished (which causes it to display the finishing-text)
        {
            roomMaintainerScript.finished = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used on the MusicRooms to handle the AudioTrack placement

public class RoomMusicController : MonoBehaviour
{
    private GameObject sceneMaster;
    private RoomMaintainer roomMaintainerScript;

    private GameObject audioTrack;

    public bool ambient;        //a flag used to distinguish black-metal from techno MusicRooms, this is public so it can be set individually and the same script can be used for both rooms

    private int tracknumber;
    // Start is called before the first frame update
    void Start()
    {   
        sceneMaster = GameObject.Find("SceneMaster");
        roomMaintainerScript = sceneMaster.GetComponent<RoomMaintainer>();      //this gets the RoomMaintainerScript which contains some variables used here

        tracknumber = roomMaintainerScript.tracksLeft - 1;      //at startup, the number of the track used for this specific room is calculated, see RoomMaintainerScript for more info

        if(ambient && !roomMaintainerScript.aListEmpty && tracknumber >= 0)     //if this is a blackmetal-room and there are still tracks left and the tracknumber calculated at startup of this script is not below zero:
        {
            audioTrack = roomMaintainerScript.ambienceTracks[tracknumber];      //take a track from the Black-Metal trackList of the RoomMaintainer at the itemNumber of "tracknumber"
            audioTrack.transform.position = transform.position;                 //and move it to the middle of this room
        }
        else if(!roomMaintainerScript.tListEmpty && tracknumber >= 0)       //if this is a TechnoRoom and there are still tracks left and the tracknumber calculated at startup of this script is not below zero:
        {
            audioTrack = roomMaintainerScript.technoTracks[tracknumber];    //do the same as above but take a Track from the TechnoTrack list instead
            audioTrack.transform.position = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
                //this script does nothing once the startup-process is finished
                //it only moves a Track inside the room
    }
}

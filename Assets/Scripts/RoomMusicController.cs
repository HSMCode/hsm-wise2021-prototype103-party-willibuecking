using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMusicController : MonoBehaviour
{
    private GameObject sceneMaster;
    private RoomMaintainer roomMaintainerScript;

    private GameObject audioTrack;

    public bool ambient;

    private int tracknumber;
    // Start is called before the first frame update
    void Start()
    {   
        sceneMaster = GameObject.Find("SceneMaster");
        roomMaintainerScript = sceneMaster.GetComponent<RoomMaintainer>();

        tracknumber = roomMaintainerScript.tracksLeft - 1;
        Debug.Log(tracknumber);
        Debug.Log(roomMaintainerScript.tracksLeft);

        if(ambient && !roomMaintainerScript.aListEmpty && tracknumber >= 0)
        {
            //audioTrack = roomMaintainerScript.ambienceTracks[Random.Range(0, roomMaintainerScript.ambienceTracks.Count)];
            audioTrack = roomMaintainerScript.ambienceTracks[tracknumber];
            audioTrack.transform.position = transform.position;
        }
        else if(!roomMaintainerScript.tListEmpty && tracknumber >= 0)
        {
            //audioTrack = roomMaintainerScript.technoTracks[Random.Range(0, roomMaintainerScript.technoTracks.Count)];
            audioTrack = roomMaintainerScript.technoTracks[tracknumber];
            audioTrack.transform.position = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            /* roomMaintainerScript.ambienceTracks.Remove(audioTrack);
            roomMaintainerScript.technoTracks.Remove(audioTrack); */
        }
    }
}

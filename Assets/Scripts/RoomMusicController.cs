using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMusicController : MonoBehaviour
{
    private GameObject sceneMaster;
    private RoomMaintainer roomMaintainerScript;

    private GameObject audioTrack;

    public bool ambient;
    // Start is called before the first frame update
    void Start()
    {
        sceneMaster = GameObject.Find("SceneMaster");
        roomMaintainerScript = sceneMaster.GetComponent<RoomMaintainer>();

        if(ambient)
        {
            audioTrack = roomMaintainerScript.ambienceTracks[Random.Range(0, roomMaintainerScript.ambienceTracks.Count)];
        }
        else
        {
            audioTrack = roomMaintainerScript.technoTracks[Random.Range(0, roomMaintainerScript.technoTracks.Count)];
        }

        audioTrack.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            roomMaintainerScript.ambienceTracks.Remove(audioTrack);
            roomMaintainerScript.technoTracks.Remove(audioTrack);
        }
    }
}

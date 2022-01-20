using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMaintainer : MonoBehaviour
{
    public List<GameObject> partyRooms;
    public GameObject musicRoomA;
    public GameObject musicRoomT;
    public GameObject emptyRoom;

    public List<GameObject> ambienceTracks;
    public List<GameObject> technoTracks;

    public bool aListEmpty;
    public bool tListEmpty;

    // Start is called before the first frame update
    void Start()
    {
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
    }
}
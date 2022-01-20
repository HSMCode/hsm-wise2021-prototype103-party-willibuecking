using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerBody");
        playerPos = GameObject.Find("PlayerBody").transform.position;
        thisSource = GetComponent<AudioSource>();
        initVol = thisSource.volume;

    }

    // Update is called once per frame
    void Update()
    {
        if(isCollected)
        {
            thisSource.volume = initVol - 0.3f;
            thisSource.spatialBlend = 0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && firstEnter)
        {
            isCollected = true;
            transform.parent = player.transform;
            firstEnter = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "MusicSwitcher" && !isCollected)
        {
            transform.position = new Vector3(0f, 50f, 0f);
        }
    }
}

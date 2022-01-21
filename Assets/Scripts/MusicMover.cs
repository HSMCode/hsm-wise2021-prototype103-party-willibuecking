using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMover : MonoBehaviour
{

    public Color lightColor;
    public Light thisLight;

    private bool isCollected = false;
    private GameObject player;
    private Vector3 playerPos;

    private GameObject sceneMaster;
    private RoomMaintainer roomMaintainerScript;

    private bool firstEnter = true;

    AudioSource thisSource;
    private float initVol;

    public int pointsA;
    public int pointsT;

    public float lightSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerBody");
        playerPos = GameObject.Find("PlayerBody").transform.position;
        thisSource = GetComponent<AudioSource>();
        initVol = thisSource.volume;
        thisLight.color = lightColor;

    }

    // Update is called once per frame
    void Update()
    {

        thisLight.intensity = Mathf.PingPong(Time.time*lightSpeed, 1f);
        if(isCollected)
        {
            thisSource.volume = initVol - 0.5f;
            //thisSource.spatialBlend = 0f;
        }

        if(transform.position.y >10)
        {
            thisSource.volume = 0;
        }
        else
        {
            thisSource.volume = initVol;
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
        if(other.tag == "FinishRoom")
        {
            transform.parent = other.gameObject.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "MusicSwitcher" && !isCollected)
        {
            transform.position = new Vector3(0f, 100f, 0f);
        }
    }
}

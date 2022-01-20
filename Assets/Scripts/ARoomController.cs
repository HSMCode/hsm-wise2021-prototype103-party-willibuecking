using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        InvokeRepeating("StrobeInit", 2f, 5f);
        strobe.enabled = false;

        sceneMaster = GameObject.Find("SceneMaster");
        roomMaintainerScript = sceneMaster.GetComponent<RoomMaintainer>();
    }

    // Update is called once per frame
    void Update()
    {
        redLight.intensity = Mathf.PingPong(Time.time*0.5f, 1.5f);
    }

    void StrobeInit()
    {
        strobeCounter = 50;
        StartCoroutine("StrobeEffect");
    }

    IEnumerator StrobeEffect()
    {
        yield return new WaitForSeconds(0.025f);
        strobe.enabled = !strobe.enabled;
        strobeCounter --;
        if(strobeCounter > 0)
        {
            StartCoroutine("StrobeEffect");
        }
        else
        {
            strobe.enabled = false;
        }
    }
    void OnTriggerEnter (Collider other)
    {
        if(other.tag == "AudioTrack")
        {
            roomMaintainerScript.score += other.gameObject.GetComponent<MusicMover>().pointsA;
            roomMaintainerScript.score -= other.gameObject.GetComponent<MusicMover>().pointsT;
        }
        if(other.tag == "Player")
        {
            roomMaintainerScript.finished = true;
        }
    }
}

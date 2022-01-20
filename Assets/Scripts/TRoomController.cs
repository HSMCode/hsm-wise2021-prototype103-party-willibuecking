using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRoomController : MonoBehaviour
{
    public GameObject lightRotator;
    public GameObject strobe;
    private bool strobeState =true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StrobeEffect");
    }

    // Update is called once per frame
    void Update()
    {
      lightRotator.transform.Rotate(0.0f, 90.0f*Time.deltaTime, 0.0f, Space.Self);
      
      strobe.SetActive(strobeState);
    }

    IEnumerator StrobeEffect()
    {
        yield return new WaitForSeconds(0.25f);
        strobeState = !strobeState;
        StartCoroutine("StrobeEffect");
    }
}

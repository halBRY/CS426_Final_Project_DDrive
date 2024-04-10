using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioSwitch : MonoBehaviour
{
    public AudioClip myTrack;
    public GameObject myPlayer;

    public bool isFinal = false;

    public void OnTriggerEnter(Collider other)
    {
        if(!isFinal)
        {
            if(other.gameObject.tag == "Player")
            {
                myPlayer.GetComponent<AudioSource>().clip = myTrack;
                myPlayer.GetComponent<AudioSource>().Play();
            }
        }

        if(isFinal)
        {
            Debug.Log("Game is over");
            GameObject trackTime = GameObject.FindWithTag("TrackTime");
            trackTime.GetComponent<TrackTime>().endGame();
        }
    }
}

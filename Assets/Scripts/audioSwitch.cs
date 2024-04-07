using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioSwitch : MonoBehaviour
{
    public AudioClip myTrack;
    public GameObject myPlayer;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            myPlayer.GetComponent<AudioSource>().clip = myTrack;
            myPlayer.GetComponent<AudioSource>().Play();
        }
    }
}

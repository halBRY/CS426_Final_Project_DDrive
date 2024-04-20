using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject myNotePrefab;
   
    public bool noteHit = false;
    public bool trackTime = true;

    public float startTime;
    public float currentTime;
    public float endTime;

    void Start()
    {
        //spawn first note
        Instantiate(myNotePrefab, gameObject.transform.position, gameObject.transform.rotation);
    }

    void Update()
    {
        //If a note is hit, it will respawn after 8 seconds. 
        if(noteHit && trackTime)
        {
            startTime = Time.time;
            endTime = Time.time + 8f;
            trackTime = false;
        }

        if(noteHit && Time.time > endTime)
        {
            trackTime = true;
            noteHit = false;
            Instantiate(myNotePrefab, gameObject.transform.position, gameObject.transform.rotation);
        }
    }

    void NoteDestroyed()
    {
        noteHit = true;
    }
}

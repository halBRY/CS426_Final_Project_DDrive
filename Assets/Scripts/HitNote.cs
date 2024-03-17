using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitNote : MonoBehaviour
{
    public AccuracyManager accuracyManager;
    public PlayerController player;

    private bool canHit = false;
    private bool checkingForHits = true;
    private bool missedNote = false;

    // Each fram checks if player is in trigger, if so check player is pressing hit button
    void Update()
    {
        if(canHit)
        {
            if(player.getAttemptHit())
            {
                if(gameObject.tag == "HitCenter")
                {
                    Debug.Log("hit perfect");
                    accuracyManager.Perfect();
                    Destroy(transform.parent.gameObject);
                    player.addHit();
                    canHit = false;
                }
            }
            checkingForHits = false;
        }
        else
        {
            checkingForHits = true;
        }
    }

    void OnTriggerEnter (Collider collider)
    {
        canHit = true;
    }

    void OnTriggerExit(Collider collider)
    {
        if(!missedNote)
        {
            missedNote = true;
            Destroy(transform.parent.gameObject);
            Debug.Log("missed note");
            canHit = false;
            accuracyManager.ResetCombo();
            player.MissedHit();
        }
    }
    

}

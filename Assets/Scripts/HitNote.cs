using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitNote : MonoBehaviour
{
    public AccuracyManager accuracyManager;
    public PlayerController player;

    private bool canHit = false;
    private bool checkingForHits = true;

    // Each fram checks if player is in trigger, if so check player is pressing hit button
    void Update()
    {
        if(canHit)
        {
            if(player.getAttemptHit())
            {
                Debug.Log("hit");
                accuracyManager.AccuracyInc();
                Destroy(gameObject);
                canHit = false;
            }

            if(checkingForHits)
            {
                player.addHit();
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
        canHit = false;
    }
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldNote : MonoBehaviour
{
    public AccuracyManager accuracyManager;
    public PlayerController player;

    private bool canHit = false;
    private bool missedNote = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canHit)
        {
            if(player.getAttemptHit())
            {
                Debug.Log("hit hold");
                accuracyManager.Perfect();
                Destroy(gameObject);
                canHit = false;
            }
        }
    }

    void OnTriggerEnter (Collider collider)
    {
        canHit = true;
    }

    void OnTriggerExit(Collider collider)
    {
        // In if to prevent a note being counted twice if missed
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

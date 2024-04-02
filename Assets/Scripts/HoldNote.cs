using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldNote : MonoBehaviour
{
    public AccuracyManager accuracyManager;
    public PlayerController player;
    public HoldNote startHold;

    private bool canHit = false;
    private bool missedNote = false;
    // Start is called before the first frame update
    private bool hitStart;
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
                Debug.Log("hold hit");
                accuracyManager.Perfect();
                gameObject.GetComponent<Renderer>().enabled = false;
                hitStart = true;
                canHit = false;
            }
        }
        else if (hitStart && !canHit)
        {
            Debug.Log("add a way to keep track of player holding keydown");
        }
    }

    void OnTriggerEnter (Collider collider)
    {
        
        if(gameObject.tag == "HoldStart")
        {
            canHit = true;
        }
        else if (getStart() && gameObject.tag == "HoldEnd")
        {
                Debug.Log("Hold hit end");
                accuracyManager.Perfect();
                Destroy(transform.parent.gameObject);
        }
        else if (getStart() && gameObject.tag == "HoldCenter")
        {
                Debug.Log("Hold hit center");
                accuracyManager.Perfect();
                Destroy(gameObject);
        }
     

    }

    void OnTriggerExit(Collider collider)
    {
        // In if to prevent a note being counted twice if missed
        if(!missedNote && !getStart())
        {
            missedNote = true;
            Destroy(transform.parent.gameObject);
            Debug.Log("missed note");
            canHit = false;
            accuracyManager.ResetCombo();
            player.MissedHit();
        }
    }

    bool getStart()
    {
        return startHold.hitStart;
    }
}

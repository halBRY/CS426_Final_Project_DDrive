using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldNote : MonoBehaviour
{
    public AccuracyManager accuracyManager;
    public PlayerController player;
    public HoldNote startHold;

    private bool canHit = false;
    private bool holding = false;
    private bool missedNote = false;
    // Start is called before the first frame update
    private bool hitStart;

    private Color emissionColor = Color.blue;

    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(canHit)
        {
            if(player.getAttemptHit())
            {
                if(gameObject.tag == "HoldStart")
                {
                    Debug.Log("hold hit");
                    accuracyManager.HoldNote();
                    player.addHit();
                    player.playHitSound();
                    gameObject.GetComponent<Renderer>().enabled = false;
                    Destroy(transform.parent.Find("HoldEarly").gameObject);
                    hitStart = true;
                    canHit = false;
                }
                else if (gameObject.tag == "HoldEarly")
                {
                    Debug.Log("hold hit");
                    accuracyManager.HoldNote();
                    player.addHalfHit();
                    player.playHitSound();
                    gameObject.GetComponent<Renderer>().enabled = false;
                    hitStart = true;
                    canHit = false;
                }
            }
            else if (getStart() && player.getAttemptHold())
            {
                if(gameObject.tag == "HoldEnd")
                {
                    Debug.Log("Hold hit end");
                    accuracyManager.HoldNote();
                    player.addHit();
                    Destroy(transform.parent.gameObject);
                }
                else if (gameObject.tag == "HoldCenter")
                {
                    Debug.Log("Hold hit center");
                    accuracyManager.HoldNote();
                    player.addHit();
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnTriggerEnter (Collider collider)
    {
        
        if(gameObject.tag == "HoldStart")
        {
            SetEmissionColor(emissionColor);
            canHit = true;
        }
        else if(gameObject.tag == "HoldEarly")
        {
            canHit = true;
        }
        else if (getStart() && gameObject.tag == "HoldEnd")
        {
            canHit = true;
            if(!player.getAttemptHold())
            {
                missedNote = true;
                Destroy(transform.parent.gameObject);
                Debug.Log("missed note");
                canHit = false;
                accuracyManager.ResetCombo();
                player.MissedHit();
            }
        }
        else if (getStart() && gameObject.tag == "HoldCenter")
        {
            canHit = true;
            if(!player.getAttemptHold())
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

    void OnTriggerExit(Collider collider)
    {
        // In if to prevent a note being counted twice if missed
        SetEmissionColor(Color.black);
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

    private void SetEmissionColor(Color color)
    {
        // Set the emission color of the material
        material.SetColor("_EmissionColor", color);
        // Activate emission
        material.EnableKeyword("_EMISSION");
    }
}
